using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SoundBarkController))]
public class SoundBarkController_Editor : Editor {

    SoundBarkController self;

    void OnEnable() {
        self = (SoundBarkController)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (GUILayout.Button("Invoke Bark")) {
            self.PlayRandomBark();
        }
    }

}
