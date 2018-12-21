using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyNinjaMovement : MonoBehaviour
{
	private Transform waypoint;

	private int waypointIndex = 0;

	private Animator anim;

	private float vertical;

	private float rotation;

	public float speed = 1.5f;

	public float rotationSpeed = 5f;

	public float accuracyWP = 2.0f;

	void Start()
	{
		waypoint = Waypoints.points[0];

		anim = GetComponent<Animator>();

		anim.SetBool("run", true);
	}

	void Update()
	{
		Vector3 dir = waypoint.position - anim.transform.position;
		dir.y = 0;


		if (Vector3.Distance(waypoint.position, transform.position) < accuracyWP)
		{
			Debug.Log("Getting Next waypoint");
			GetNextWaypoint();
		}

		Vector3 direction = Waypoints.points[waypointIndex].transform.position - transform.position;

		this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

		this.transform.Translate(0, 0, Time.deltaTime * speed);
	}


	void GetNextWaypoint()
	{
		if (waypointIndex >= Waypoints.points.Length - 1)
		{
			EndPath();
			return;
		}
		
		waypointIndex++;

		waypoint = Waypoints.points[waypointIndex];
	}


	void EndPath()
	{
		PlayerStats.Lives--;

		WaveSpawner.EnemiesAlive--;

		Destroy(gameObject);
	}

}
