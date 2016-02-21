using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class paletteColor : MonoBehaviour, IPointerEnterHandler {

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
	}

}
