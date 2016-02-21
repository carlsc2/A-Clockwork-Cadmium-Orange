using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class MissileController : MonoBehaviour {

	Animator animController;

	void OnEnable() {
		animController = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Launch() {
		animController.SetTrigger("Trigger_LaunchMissile");
		GetComponent<AudioSource>().Play();
	}

	public void EndGame() {
		UnityEngine.SceneManagement.SceneManager.LoadScene("end1");
	}
}
