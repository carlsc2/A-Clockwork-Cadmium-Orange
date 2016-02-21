using UnityEngine;
using System.Collections.Generic;

public class enemyAI : MonoBehaviour {

	public float shootradius = 10f;
	public float sightradius = 30f;

	private Transform player;

	public GameObject projectile;

	private NavMeshAgent agent;

	public List<Transform> waypoints;

	private int index = 0;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		float pdist = Vector3.Distance(transform.position, player.position);
		print(pdist);
		if (pdist < sightradius) {
			if (pdist < shootradius) {
				agent.Stop();
				shootPlayer();
			}
			else {
				agent.Resume();
				agent.SetDestination(player.position);
			}
		}
		else {
			if (waypoints.Count > 0) {
				agent.Resume();
				agent.SetDestination(waypoints[index].position);
				if (Vector3.Distance(transform.position, waypoints[index].position) < 2) {
					index = ++index % waypoints.Count;
					agent.SetDestination(waypoints[index].position);
				}
			}
		}
	
	}

	void shootPlayer() {
		Ray ray = new Ray(transform.position, player.position - transform.position);
		//Debug.DrawRay(ray.origin, ray.direction);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {
			if(hit.collider && hit.collider.transform.root.tag == "Player") {
				GameObject bullet = Instantiate(projectile, transform.position - transform.forward, Quaternion.identity) as GameObject;
				bullet.GetComponent<Rigidbody>().AddForce((-transform.forward) * 500);
			}
			
		}
		
	}
}
