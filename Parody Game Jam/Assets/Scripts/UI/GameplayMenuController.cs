using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class GameplayMenuController : MonoBehaviour {

    private Animator _animController;
    private string _menuIsActiveID = "MenuIsActive_Bool";

    public KeyCode pauseMenuKey = KeyCode.Escape;

    void Awake() {
        _animController = GetComponent<Animator>();
    }

    void Update() {
        HandleInput();
    }

    private void HandleInput() {
        if (Input.GetKeyDown(pauseMenuKey)) {

            HandlePauseMenu();

        }
    }

    private void HandlePauseMenu() {
        bool currentMenuSetting = _animController.GetBool(_menuIsActiveID);

        //want to resume game
        if (currentMenuSetting == true) {
            ResumeGame();
        }
        //want to pause game
        else {
            PauseGame();
        }

    }

    public void ResumeGame() {
        Time.timeScale = 1.0f;//resume time

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        _animController.SetBool(_menuIsActiveID, false);//turn off menu, turn on gameplay hud
    }

    public void PauseGame() {
        Time.timeScale = 0.0f;//pause time

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _animController.SetBool(_menuIsActiveID, true);//turn off gameplay hud, turn on men
    }
}
