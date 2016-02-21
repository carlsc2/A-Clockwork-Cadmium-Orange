using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class MovementMotor : MonoBehaviour {

    Rigidbody rigBod;

    public Vector3 desiredDirec;
    public Vector3 trueDirec;

    public float redirectSpeed = 1.0f;

    public float maxMoveSpeed = 1.0f;

    void Awake() {
        rigBod = GetComponent<Rigidbody>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        UpdateTrueDirec();

	}

    void FixedUpdate() {
        ApplyMovement_Fixed();

    }

    public void SetDesiredMoveDirec(Vector3 desiredDirec) {
        this.desiredDirec = desiredDirec;
    }

    public void SetDesiredRedirectSpeed(float redirectSpeed) {
        this.redirectSpeed = redirectSpeed;
    }

    private void UpdateTrueDirec() {
        if (trueDirec == desiredDirec) { return; }

        trueDirec = Vector3.MoveTowards(trueDirec, desiredDirec, redirectSpeed * Time.deltaTime);
    }

    private void ApplyMovement_Fixed() {
        //transform.Translate(trueDirec);

        //Vector3 adjustedTrueDirec = transform.localToWorldMatrix * trueDirec;
        //rigBod.MovePosition(transform.position + (Vector3)(transform.localToWorldMatrix * trueDirec * maxMoveSpeed));
        Vector3 finalTrueDirec = transform.localToWorldMatrix * (new Vector3(trueDirec.x, 0.0f, trueDirec.z) * maxMoveSpeed);
        rigBod.velocity = new Vector3(finalTrueDirec.x, rigBod.velocity.y, finalTrueDirec.z);
    }
}
