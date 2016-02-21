using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(AudioSource))]

public class TitleAudio : MonoBehaviour {

	public AudioClip background_intro;
	public AudioClip background;

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource>().loop = true;
		StartCoroutine (playBackground());
	}

	IEnumerator playBackground(){
		GetComponent<AudioSource>().clip = background_intro;
		GetComponent<AudioSource>().Play ();
		yield return new WaitForSeconds (GetComponent<AudioSource>().clip.length);
		GetComponent<AudioSource>().clip = background;
		GetComponent<AudioSource>().Play ();
	}

}
