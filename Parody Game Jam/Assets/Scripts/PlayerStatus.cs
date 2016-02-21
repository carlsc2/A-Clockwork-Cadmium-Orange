using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	public int health = 100;
	public float maxHealth = 100;
	public long score = 0;


	public void Hit() {
		if (health > 0) {
			health--;
		}
	}
	public void AddScore(long points) {
		score += points;
	}



}
