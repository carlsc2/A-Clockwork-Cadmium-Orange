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
            self.PlayRandomBark(BarkClipInfo.BarkTag.None);
        }

        if (GUILayout.Button("Transfer Audio Clilps")) {
            //self.barkClipStorage.ClearStorage();
            self.barkClipStorage = new BarkClipStorage(self.TransferList.ToArray());
        }
    }

}
