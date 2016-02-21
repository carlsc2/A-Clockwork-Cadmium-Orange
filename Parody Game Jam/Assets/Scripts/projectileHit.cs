using UnityEngine;
using System.Collections;

public class projectileHit : MonoBehaviour {

	private Vector3 startpoint;

	void Start() {
		startpoint = transform.position;
	}

	void OnTriggerEnter(Collider col) {
		if(col.gameObject.tag != "projectile") {
			Destroy(gameObject);
			//make player lose hp
			PlayerStatus pstat = col.transform.root.GetComponent<PlayerStatus>();
			if (pstat) {
				pstat.Hit();
			}
		}


	}

	void Update() {
		if(Vector3.Distance(transform.position,startpoint) > 100) {
			Destroy(gameObject);
		}
	}
}
