﻿using UnityEngine;
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

        ApplyMovement();
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

    private void ApplyMovement() {
        //transform.Translate(trueDirec);
        //rigBod.MovePosition(transform.position + trueDirec);
        rigBod.velocity = transform.localToWorldMatrix * (trueDirec * maxMoveSpeed);
    }
}
