using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MovementMotor))]
[RequireComponent(typeof(LookController))]
public class PlayerController : MonoBehaviour {

    public enum InteractionMode {
        Movement = 0,
        Painting = 1,

    }

    public LookController lookControl;
    public MovementMotor motor;
    
    public InteractionMode curInteractionMode;


    void Awake() {
        lookControl = GetComponent<LookController>();
        motor = GetComponent<MovementMotor>();
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
}
