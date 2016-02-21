using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PaintScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

	public static GameObject itemBeingDragged;
	private Vector3 startPosition;
	private Transform startParent;
	private Texture2D canvastex;

	private Vector2 prevpos;

	public Image drawingCanvas;
	public RectTransform brush;

	private LookController playercontrol;

	private bool dragging = false;

	private List<Vector2> drawpoints;

	private Transform player;

	public Color paintColor;

	public Image brushtip;

	public GameObject[] trees;
	public GameObject[] clouds;
	public GameObject[] mountains;

	private PlayerStatus pstat;

	private int colorsused = 1;

	private Color prevcolor;

	private AudioSource aso;

	void Awake() {
		aso = GetComponent<AudioSource>();
		Cursor.lockState = CursorLockMode.Locked;
		drawpoints = new List<Vector2>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playercontrol = player.GetComponent<LookController>();
		pstat = player.GetComponent<PlayerStatus>();

		Color[] cols = new Color[128 * 128];
		for (int i = 0; i < 128 * 128; i++) {
			cols[i] = Color.clear;
		}
		canvastex = new Texture2D(128, 128);
		drawingCanvas.sprite = Sprite.Create(canvastex, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f));
		canvastex.SetPixels(cols);
		canvastex.Apply();
	}

	void Update() {
		//have to check in update because can't drag if mouse is locked
		if (Input.GetMouseButton(1)) {
			Cursor.lockState = CursorLockMode.None;
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPaused = true;
#else
			Application.Quit();
#endif

		}
	}

	public void OnBeginDrag(PointerEventData eventData) {
		if (eventData.button == PointerEventData.InputButton.Right) {
			playercontrol.canLookAround = false;
			drawingCanvas.enabled = true;
			startPosition = brush.position;
			canvastex = new Texture2D(128, 128);
			//canvastex.filterMode = FilterMode.Point;
			drawingCanvas.sprite = Sprite.Create(canvastex, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f));
			prevpos = Input.mousePosition;
		
			Color[] cols = new Color[128 * 128];
			for (int i = 0; i < 128 * 128; i++) {
				cols[i] = Color.clear;
			}
			canvastex.SetPixels(cols);
			canvastex.Apply();
			dragging = true;

			Time.timeScale = .1f;
			Time.fixedDeltaTime = .1f * 0.02f;
			aso.pitch = 0.5f;
		}
	}

	public void OnDrag(PointerEventData eventData) {
		if (dragging) {
			brush.position = Input.mousePosition;
			if (eventData.button == PointerEventData.InputButton.Left) {
				if(prevcolor != paintColor) {
					prevcolor = paintColor;
					colorsused++;
				}

				Bresenham3D line = new Bresenham3D(prevpos, Input.mousePosition);
				foreach (Vector3 point in line) {
					Vector2 tmp = Camera.main.ScreenToViewportPoint(point);
					drawpoints.Add(tmp);
					canvastex.SetPixel((int)(tmp.x * 128), (int)(tmp.y * 128), paintColor);

					//fill in neighbors to make line thicker
					canvastex.SetPixel((int)((tmp.x+1) * 128), (int)(tmp.y * 128), paintColor);
					canvastex.SetPixel((int)((tmp.x-1) * 128), (int)(tmp.y * 128), paintColor);
					canvastex.SetPixel((int)(tmp.x * 128), (int)((tmp.y+1) * 128), paintColor);
					canvastex.SetPixel((int)(tmp.x * 128), (int)((tmp.y-1) * 128), paintColor);
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

			Time.timeScale = 1f;
			Time.fixedDeltaTime = 1f * 0.02f;
			aso.pitch = 1f;

			if (drawpoints.Count > 0) destroyAll(ConvexHull.ComputeConvexHull(drawpoints, false), CompareShape.Match(drawpoints));

			//foreach (Vector2 point in ConvexHull.ComputeConvexHull(drawpoints,false)) {
				//canvastex.SetPixel((int)(point.x * 128), (int)(point.y * 128), Color.blue);
			//}
			//canvastex.Apply();
			drawpoints.Clear();
			colorsused = 1;
			prevcolor = paintColor;
		}
	}

	void destroyAll(List<Vector2> hull, string mode) {
		long points = 0;
		int kills = 0;
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("enemy")) {
			Ray ray = new Ray(player.transform.position, g.transform.position - player.transform.position);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1<<8))) { //ignore projectile layer
				//print(hit.collider.gameObject);
				if (hit.collider == g.GetComponentInChildren<Collider>()) {
					Vector2 spos = Camera.main.WorldToViewportPoint(g.transform.position);
					if (ConvexHull.ContainsPoint(hull, spos)) {
						
						switch (mode) {
							case "tree":
								Instantiate(trees[Random.Range(0, trees.Length)], g.transform.position, Quaternion.identity);
								break;
							case "mountain":
								Instantiate(mountains[Random.Range(0, mountains.Length)], g.transform.position, Quaternion.identity);
								break;
							case "cloud":
								Instantiate(clouds[Random.Range(0, clouds.Length)], g.transform.position, Quaternion.identity);
								break;

						}
						points += 100 * ++kills;
						Destroy(g);
					}
				}
			}
		}
		if(kills > 1) {
			SoundBarkController.singleton.PlayRandomBark(BarkClipInfo.BarkTag.Multi);
		}
		else {
			SoundBarkController.singleton.PlayRandomBark(BarkClipInfo.BarkTag.Tree);
		}
		//print("Colors used: " + colorsused);
		pstat.AddScore(points * colorsused);

		/*foreach (GameObject g in GameObject.FindGameObjectsWithTag("projectile")) {
			Ray ray = new Ray(player.transform.position, g.transform.position - player.transform.position);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				if (hit.collider == g.GetComponentInChildren<Collider>()) {
					Vector2 spos = Camera.main.WorldToViewportPoint(g.transform.position);
					if (ConvexHull.ContainsPoint(hull, spos)) {
						Destroy(g);
					}
				}
			}
		}*/



	} 

}
