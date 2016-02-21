using UnityEngine;
using System.Collections;

public class ComputerInteract : MonoBehaviour {

	private AudioSource aso;

	public AudioClip clip1;
	public AudioClip clip2;
	public AudioClip clip3;
	public AudioClip clip4;

	private bool started = false;

	void Awake() {
		aso = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider col) {

		if(!started && col.transform.root.tag == "Player") {
	
			StartCoroutine(audioroutine());
		}
	}

	IEnumerator audioroutine() {
		started = true;
		aso.PlayOneShot(clip1);
		yield return new WaitForSeconds(clip1.length);
		aso.PlayOneShot(clip2);
		yield return new WaitForSeconds(clip2.length);
		aso.PlayOneShot(clip3);
		yield return new WaitForSeconds(clip3.length);
		while (true) {
			aso.PlayOneShot(clip4);
			yield return new WaitForSeconds(clip4.length + 2);
		}
	}
}
