using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour {
	private Transform target;

	void Awake() {
		target = Camera.main.transform;
	}

	void Update() {

		Vector3 targetPostition = new Vector3(target.position.x,
										transform.position.y,
										target.position.z);
		transform.LookAt(targetPostition);
		transform.rotation = Quaternion.Euler(new Vector3(0, 180+transform.rotation.eulerAngles.y, 0));


	}
}