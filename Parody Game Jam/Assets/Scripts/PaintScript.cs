using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PaintScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

	public static GameObject itemBeingDragged;
	private Vector3 startPosition;
	private Transform startParent;
	private Texture2D canvastex;

	private Vector2 prevpos;

	public Image drawingCanvas;
	public RectTransform brush;

	void Update() {
		if (Input.GetKeyDown(KeyCode.E)) {
			print("ayy lmao");
		}

	}

	public void OnBeginDrag(PointerEventData eventData) {
		drawingCanvas.enabled = true;
		startPosition = brush.position;
		canvastex = new Texture2D(128, 128);
		canvastex.filterMode = FilterMode.Point;
		drawingCanvas.sprite = Sprite.Create(canvastex, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f));
		prevpos = Input.mousePosition;

		Color[] cols = new Color[128 * 128];
		for (int i = 0; i < 128 * 128; i++) {
			cols[i] = Color.clear;
		}

		canvastex.SetPixels(cols);
		canvastex.Apply();
	}

	public void OnDrag(PointerEventData eventData) {
		brush.position = Input.mousePosition;

		Bresenham3D line = new Bresenham3D(prevpos, Input.mousePosition);

		foreach(Vector3 point in line) {
			Vector2 tmp = Camera.main.ScreenToViewportPoint(point);
			canvastex.SetPixel((int)(tmp.x * 128), (int)(tmp.y * 128), Color.red);
		}
		canvastex.Apply();

		prevpos = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData) {
		brush.position = startPosition;

		Color[] cols = new Color[128 * 128];
		for (int i = 0; i < 128 * 128; i++) {
			cols[i] = Color.clear;
		}

		canvastex.SetPixels(cols);
		canvastex.Apply();
	}

}
