using UnityEngine;
using System.Collections;

public class projectileHit : MonoBehaviour {

	private Vector3 startpoint;

	void Start() {
		startpoint = transform.position;
	}

	void OnCollisionEnter(Collision col) {
		if(col.gameObject.tag != "projectile") {
			Destroy(gameObject);
		}
		//make player lose hp
	}

	void Update() {
		if(Vector3.Distance(transform.position,startpoint) > 100) {
			Destroy(gameObject);
		}
	}
}
