using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MovementMotor))]
[RequireComponent(typeof(LookController))]
[RequireComponent(typeof(PlayerAnimStateMachineController))]

//[RequireComponent(typeof(PlayerCamController))]
public class PlayerController : MonoBehaviour {

	public enum InteractionMode {
		Movement = 0,
		Painting = 1,

	}

	private Rigidbody rigBod;

	public LookController lookControl;
	public MovementMotor motor;
    PlayerAnimStateMachineController animationBinder;

    public PlayerCamController camController;
	
	public InteractionMode curInteractionMode;


	void Awake() {
		rigBod = GetComponent<Rigidbody>();

		lookControl = GetComponent<LookController>();
		motor = GetComponent<MovementMotor>();

        animationBinder = GetComponent<PlayerAnimStateMachineController>();
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput();
	}

	private void HandleInput() {
		HandleLookInput();

		HandleMovementInput();
	}

	private void HandleLookInput() {
	   lookControl.SetDeltaAimDirec(new Vector2(Input.GetAxisRaw("Mouse X"),
												Input.GetAxisRaw("Mouse Y")));
	}

	private void HandleMovementInput() {
		motor.SetDesiredMoveDirec(new Vector3(Input.GetAxisRaw("Horizontal"),
											  0.0f,
											  Input.GetAxisRaw("Vertical")));
	}

	public void SwitchModes(InteractionMode newMode) {
		if (curInteractionMode == newMode) { return; }

		switch(newMode) {
			case InteractionMode.Movement:
				SwitchToMovementModeProtocol();
				break;
			case InteractionMode.Painting:
				SwitchToPaintingModeProtocol();
				break;
		}

	}

	private void SwitchToMovementModeProtocol() {

	}

	private void SwitchToPaintingModeProtocol() {

	}

	public void KillPlayer() {
		rigBod.constraints = RigidbodyConstraints.None;
        rigBod.useGravity = true;
		rigBod.AddTorque(rigBod.transform.right * 50);
		motor.enabled = false;
        animationBinder.canAnimate = false;

		//lookControl.enabled = false;

		//camController.KillPlayerEffects();
	}
}
