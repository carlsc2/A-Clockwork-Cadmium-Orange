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

	public LookController playercontrol;

	private bool dragging = false;

	void Update() {
		//have to check in update because can't drag if mouse is locked
		if (Input.GetMouseButton(1)) {
			Cursor.lockState = CursorLockMode.None;
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			UnityEditor.EditorApplication.isPaused = true;
		}
	}

	public void OnBeginDrag(PointerEventData eventData) {
		if (eventData.button == PointerEventData.InputButton.Right) {
			playercontrol.canLookAround = false;
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
			dragging = true;
		}
	}

	public void OnDrag(PointerEventData eventData) {
		if (dragging) {
			brush.position = Input.mousePosition;
			if (eventData.button == PointerEventData.InputButton.Left) {
				Bresenham3D line = new Bresenham3D(prevpos, Input.mousePosition);
				foreach (Vector3 point in line) {
					Vector2 tmp = Camera.main.ScreenToViewportPoint(point);
					canvastex.SetPixel((int)(tmp.x * 128), (int)(tmp.y * 128), Color.red);
				}
				canvastex.Apply();
			}
			prevpos = Input.mousePosition;
		}
	}

	public void OnEndDrag(PointerEventData eventData) {
		if (eventData.button == PointerEventData.InputButton.Right) {
			playercontrol.canLookAround = true;
			brush.position = startPosition;

			Color[] cols = new Color[128 * 128];
			for (int i = 0; i < 128 * 128; i++) {
				cols[i] = Color.clear;
			}

			canvastex.SetPixels(cols);
			canvastex.Apply();
			dragging = false;

			Cursor.lockState = CursorLockMode.Locked;
		}
	}

}
