using UnityEngine;
using System.Collections.Generic;

public class SoundBarkController : MonoBehaviour {

    public AudioSource playerAudioSourceRef;

    public static SoundBarkController singleton = null;


    public List<AudioClip> barkClips;

    private List<AudioClip> unusedBarkClips;
    private List<AudioClip> consumedBarkClips;

    void Awake() {
        if (singleton == null) {
            singleton = this;
        }

        if (playerAudioSourceRef == null) {
            Debug.LogError("SoundBarkController: No Reference to the Player's Audio Source assigned");
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


    public void PlayRandomBark() {
        playerAudioSourceRef.PlayOneShot(barkClips[Random.Range(0, barkClips.Count - 1)]);
    }



}
