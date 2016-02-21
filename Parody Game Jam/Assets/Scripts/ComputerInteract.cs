using UnityEngine;
using System.Collections;

public class ComputerInteract : MonoBehaviour {

	private AudioSource aso;

	public AudioClip clip1;
	public AudioClip clip2;
	public AudioClip clip3;
	public AudioClip clip4;

	public GameObject ui;

	private bool started = false;

	void Awake() {
		aso = GetComponent<AudioSource>();
	}

	void OnTriggerStay(Collider col) {
		if (Input.GetKeyDown(KeyCode.E)) {
			if (!started && col.transform.root.tag == "Player") {

				StartCoroutine(audioroutine());
			}
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
		ui.SetActive(true);
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<LookController>().enabled = false;
		player.GetComponent<MovementMotor>().enabled = false;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		while (true) {
			aso.PlayOneShot(clip4);
			yield return new WaitForSeconds(clip4.length + 2);
		}
	}
}
