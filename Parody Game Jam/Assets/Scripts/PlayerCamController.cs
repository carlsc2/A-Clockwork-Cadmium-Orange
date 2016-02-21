using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityStandardAssets.ImageEffects.ColorCorrectionCurves))]
public class PlayerCamController : MonoBehaviour {
    /*
    public enum EffectsState {
        Movement = 0,
        Painting = 1,
        Dieing = 2,
    }
    */

    public float satAmount = 1.0f;

    UnityStandardAssets.ImageEffects.ColorCorrectionCurves colorCorrectionEffect;

    void OnEnable() {
        colorCorrectionEffect = GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionCurves>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        SetSaturationLevel(satAmount);
	}
    /*
    public void EffectState() {

    }
    */

    public void SetSaturationLevel(float satLevel) {
        colorCorrectionEffect.saturation = satLevel;
    }

    public void KillPlayerEffects(float killTime) {
        //StartCoroutine(KillPlayerEffects_Coroutine(killTime));
    }

    /*
    private IEnumerator KillPlayerEffects_Coroutine(float effectsTime) {
        float curKillTimer = 0.0f;

        while (curKillTimer < effectsTime) {

            float ratio = curKillTimer / effectsTime;

            curKillTimer += Time.deltaTime;


            yield return null;
        }



        yield break;
    }
    */
}
