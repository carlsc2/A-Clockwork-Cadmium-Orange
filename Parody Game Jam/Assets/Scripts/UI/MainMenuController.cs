using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

    public enum MenuPanel {
        Main = 0,
        Tutorial = 1,
    }

    private Animator _uiAnimator;

    void Awake() {
        _uiAnimator = GetComponent<Animator>();
    }

    public void GoToPanel(MenuPanel panel) {
        switch (panel) {
            case MenuPanel.Main:
                _uiAnimator.SetBool("OnTutorialScreen_Bool", false);
                break;
            case MenuPanel.Tutorial:
                _uiAnimator.SetBool("OnTutorialScreen_Bool", true);
                break;
        }
    }

    public void GoToMainPanel() {
        GoToPanel(MenuPanel.Main);
    }

    public void GoToTutorialPanel() {
        GoToPanel(MenuPanel.Tutorial);
    }

}
