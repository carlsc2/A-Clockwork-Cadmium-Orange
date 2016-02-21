using UnityEngine;
using System.Collections;

public class InWorldAudioTrigger : MonoBehaviour {

    //public AudioClip audioClip;
    public BarkClipInfo.BarkTag barkTag;
    public bool interruptSetting = false;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            HandlePlayingAudio();
        }
    }

    private void HandlePlayingAudio() {
        if (SoundBarkController.singleton != null) {
            SoundBarkController.singleton.PlayRandomBark(barkTag, interruptSetting);
        }
    }

}
