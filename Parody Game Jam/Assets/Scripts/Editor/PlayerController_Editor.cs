using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerController))]
public class PlayerController_Editor : Editor {

    PlayerController self;

    void OnEnable() {
        self = (PlayerController)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (GUILayout.Button("InvokeKill")) {
            self.KillPlayer();
        }
    }

}
