using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimStateMachineController : MonoBehaviour {

    public enum MovementAnimState {
        Stationary = 0,
        Moving = 1,
    }

    public MovementAnimState curMovementAnimState;

    Rigidbody rigBod;
    Animator animController;

    public AnimationCurve headBobMotionCurve;
    public float walkCycleTime;
    private float timer_walkCycleTime = 0.0f;


    void Awake() {

        rigBod = GetComponent<Rigidbody>();
        animController = GetComponent<Animator>();

        SwitchMovementAnimState(MovementAnimState.Stationary);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        EvaluateAnimState();

        SendHeadBobMovement();
	}

    private void EvaluateAnimState() {

        //Debug.Log(rigBod.velocity);

        Vector2 truncatedVelocity = new Vector2(
           (float)((int)(rigBod.velocity.x * 100.0f)),
           (float)((int)(rigBod.velocity.z * 100.0f)));
        
        if (Mathf.Approximately( truncatedVelocity.magnitude, 0.0f)) {
            SwitchMovementAnimState(MovementAnimState.Stationary);

            Debug.Log("stationary");
        }
        else {
            SwitchMovementAnimState(MovementAnimState.Moving);

            Debug.Log("moving");
        }
    }


    //Transition logic for different animator parameter settings
    private void SwitchMovementAnimState(MovementAnimState newState) {

        if (curMovementAnimState == newState) {
            return;
        }

        switch(newState) {
            case MovementAnimState.Stationary:
                TransitionToStationaryStateProtocol();
                break;
            case MovementAnimState.Moving:
                TransitionToMovingStateProtocol();
                break;
        }

        curMovementAnimState = newState;
    }

    private void TransitionToStationaryStateProtocol() {
        animController.SetBool("Bool_IsMoving", false);
    }

    private void TransitionToMovingStateProtocol() {
        animController.SetBool("Bool_IsMoving", true);
    }

    private void SendHeadBobMovement() {
        timer_walkCycleTime += Time.deltaTime;
        if (timer_walkCycleTime >= walkCycleTime) { timer_walkCycleTime = 0.0f; }

        animController.SetFloat("Float_HeadBobBlend", headBobMotionCurve.Evaluate(timer_walkCycleTime / walkCycleTime));

    }
}
