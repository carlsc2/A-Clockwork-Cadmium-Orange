using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class BarkClipInfo {

	public enum BarkTag {
		None = 0,
		Tree = 1,
		Multi = 2,
        WelcomeIntoHome = 3,
	}

	public AudioClip audioClip;
	public BarkTag tag;


	public BarkClipInfo(AudioClip audioClip, BarkTag tag) {
		this.audioClip = audioClip;
		this.tag = tag;
	}
}


[System.Serializable]
public class BarkClipStorage {

	public BarkClipStorage(AudioClip[] audioClips) {
		barkClipInfoList = new List<BarkClipInfo>();

		foreach (AudioClip clip in audioClips) {
			barkClipInfoList.Add(new BarkClipInfo(clip, BarkClipInfo.BarkTag.None));
		}
	}

	public List<BarkClipInfo> barkClipInfoList;

	public void GetRandomBark(BarkClipInfo.BarkTag tag, ref AudioSource originAudioSource) {
		List<AudioClip> tempBarkList = new List<AudioClip>();

		foreach (BarkClipInfo bCI in barkClipInfoList) {
			if (bCI.tag == tag) {
				tempBarkList.Add(bCI.audioClip);
			}
		}

		originAudioSource.PlayOneShot(tempBarkList[Random.Range(0, tempBarkList.Count - 1)]);
	}

	public void ClearStorage() {
		if (barkClipInfoList != null) {
			barkClipInfoList.Clear();
		}
		else {
			barkClipInfoList = new List<BarkClipInfo>();
		}
	}
}


public class SoundBarkController : MonoBehaviour {

	public List<AudioClip> TransferList;

	public AudioSource playerAudioSourceRef;

	public static SoundBarkController singleton = null;

	public BarkClipStorage barkClipStorage;

	//public List<BarkClipInfo> barkClips;

	//public List<AudioClip> barkClips;

	private List<AudioClip> unusedBarkClips;
	private List<AudioClip> consumedBarkClips;

	void Awake() {
		if (singleton == null) {
			singleton = this;
		}

		if (playerAudioSourceRef == null) {
			playerAudioSourceRef = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
			//Debug.LogError("SoundBarkController: No Reference to the Player's Audio Source assigned");
		}

		/*
		unusedBarkClips = new List<AudioClip>();
		consumedBarkClips = new List<AudioClip>();

		if (barkClips.Count > 0) {
			foreach(AudioClip clip in barkClips) {
				unusedBarkClips.Add(clip);
			}
		}
		*/
	}


	public void PlayRandomBark(BarkClipInfo.BarkTag tag, bool interrupt = false) {
        //playerAudioSourceRef.PlayOneShot(barkClips[Random.Range(0, barkClips.Count - 1)]);
        if (interrupt) {
            playerAudioSourceRef.Stop();
            barkClipStorage.GetRandomBark(tag, ref playerAudioSourceRef);
        }

        else if (!playerAudioSourceRef.isPlaying) {
			barkClipStorage.GetRandomBark(tag, ref playerAudioSourceRef);
		}
	}


}
