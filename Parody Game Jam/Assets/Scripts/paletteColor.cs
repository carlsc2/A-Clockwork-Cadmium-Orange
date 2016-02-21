using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class paletteColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public JBirdEngine.ColorLibrary.MoreColors.BobRoss.ColorPalette rossColor;

	private Color color;

	void Awake () {
		GetComponent<Image>().color = color = JBirdEngine.ColorLibrary.MoreColors.BobRoss.EnumToColor(rossColor);
	}

	public void OnPointerEnter(PointerEventData pdata) {
		print("ayy lmao: " + name);
		PaintScript ps = transform.root.GetComponent<PaintScript>();
		ps.paintColor = color;
		ps.brushtip.color = color;
		GetComponent<Image>().color = color;
	}

	public void OnPointerExit(PointerEventData data) {
		GetComponent<Image>().color = Color.black;
	}

}
