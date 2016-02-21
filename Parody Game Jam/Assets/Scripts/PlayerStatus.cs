using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	public int health = 100;
	public float maxHealth = 100;
	public long score = 0;

	private bool dead = false;


	public void Hit() {
		if (health > 0) {
			health--;
		}
		else if (!dead){
			dead = true;
			GetComponent<PlayerController>().KillPlayer();
		}
	}
	public void AddScore(long points) {
		score += points;
	}



}
