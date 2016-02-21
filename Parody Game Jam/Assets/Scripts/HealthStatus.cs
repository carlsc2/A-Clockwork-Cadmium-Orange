using UnityEngine;
using UnityEngine.UI;

public class HealthStatus : MonoBehaviour {

	public Image[] splats;
	public Image face;
	public Sprite[] faces;

	public int health = 100;
	private float maxHealth = 100;

	public Text healthText;
	public Text scoreText;

	public long score = 0;


	public void Hit() {
		if (health > 0) {
			health--;
			int splatindex = (int)((1 - health / maxHealth) * (splats.Length-1));
			int faceindex = (int)((1 - health / maxHealth) * (faces.Length-1));
			splats[splatindex].enabled = true;
			face.sprite = faces[faceindex];

			healthText.text = "Health: " + Mathf.FloorToInt(health / maxHealth).ToString() + "%";
		}
	}

	public void AddScore(long points) {
		score += points;
		scoreText.text = "Score: " + score.ToString();
	}


}
