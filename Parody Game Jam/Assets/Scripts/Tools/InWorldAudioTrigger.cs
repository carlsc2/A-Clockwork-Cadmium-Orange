using UnityEngine;
using System.Collections;

public class InWorldAudioTrigger : MonoBehaviour {

    //public AudioClip audioClip;
    public BarkClipInfo.BarkTag barkTag;
    public bool interruptSetting = false;
    //public bool oneTimeUse = true;
    private bool used = false;

    void OnTriggerEnter(Collider other) {
        if (other.transform.root.tag == "Player") {
            HandlePlayingAudio();
        }
    }

    private void HandlePlayingAudio() {
        if (!used) {
            used = true;
        }
        else { return; }

        if (SoundBarkController.singleton != null) {
            SoundBarkController.singleton.PlayRandomBark(barkTag, interruptSetting);
        }
    }

}
