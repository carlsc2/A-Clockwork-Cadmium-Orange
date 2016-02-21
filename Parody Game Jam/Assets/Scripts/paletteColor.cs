using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
[RequireComponent(typeof(AudioSource))]

public class paletteColor : MonoBehaviour, IPointerEnterHandler {

	public AudioClip red; // 1
	public AudioClip black; //7
	public AudioClip brown; //13
	public AudioClip white; //12
	public AudioClip green; // 9
	public AudioClip blue; // 9
	public AudioClip accident;
	public AudioSource source;

	public JBirdEngine.ColorLibrary.MoreColors.BobRoss.ColorPalette rossColor;

	private Color color;

	void Awake () {
		GetComponent<Image>().color = color = JBirdEngine.ColorLibrary.MoreColors.BobRoss.EnumToColor(rossColor);
	}

	public void OnPointerEnter(PointerEventData pdata) {
		PaintScript ps = transform.parent.parent.parent.GetComponent<PaintScript>();
		ps.paintColor = color;
		ps.brushtip.color = color;
		GetComponent<Image>().color = color;

		if (rossColor == JBirdEngine.ColorLibrary.MoreColors.BobRoss.ColorPalette.alizarinCrimson) {
			source.clip = red;
		} else if (rossColor == JBirdEngine.ColorLibrary.MoreColors.BobRoss.ColorPalette.midnightBlack) {
			source.clip = black;
		} else if (rossColor == JBirdEngine.ColorLibrary.MoreColors.BobRoss.ColorPalette.vanDykeBrown) {
			source.clip = brown;
		} else if (rossColor == JBirdEngine.ColorLibrary.MoreColors.BobRoss.ColorPalette.titaniumWhite) {
			source.clip = white;
		} else if (rossColor == JBirdEngine.ColorLibrary.MoreColors.BobRoss.ColorPalette.phthaloGreen) {
			source.clip = green;
		} else if (rossColor == JBirdEngine.ColorLibrary.MoreColors.BobRoss.ColorPalette.prussianBlue) {
			source.clip = blue;
		} else {
			source.clip = accident;
		}
		source.Play ();
	}

}
