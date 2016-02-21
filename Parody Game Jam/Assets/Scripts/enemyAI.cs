using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour {

	public float shootradius = 10f;

	private Transform player;

	public GameObject projectile;

	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		InvokeRepeating("shootPlayer", 0, .1f);
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position, player.position) < shootradius) {
			agent.Stop();
			shootPlayer();
		}
		else {
			agent.SetDestination(player.position);
		}
	
	}

	void shootPlayer() {
		Ray ray = new Ray(transform.position, player.position - transform.position);
		Debug.DrawRay(ray.origin, ray.direction);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {
			if(hit.collider && hit.collider.transform.root.tag == "Player") {
				GameObject bullet = Instantiate(projectile, transform.position - transform.forward, Quaternion.identity) as GameObject;
				bullet.GetComponent<Rigidbody>().AddForce((-transform.forward) * 500);
			}
			
		}
		
	}
}
