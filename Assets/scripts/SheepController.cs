﻿using UnityEngine;
using System.Collections;

public class SheepController : MonoBehaviour
{
	private NavMeshAgent navAgent;

	public float moveDistance = 1;
	public float minDistFromBorder = 10;

	void Start ()
	{
		navAgent = this.GetComponent<NavMeshAgent>();
	}

	void Update ()
	{
		if (navAgent.remainingDistance < 1) {
			float newX = transform.position.x + Random.Range(-10.0f, 10.0f) * moveDistance;
			newX = Mathf.Min(newX, GameProperties.right - minDistFromBorder);
			newX = Mathf.Max(newX, GameProperties.left + minDistFromBorder);

			float newZ = transform.position.z + Random.Range(-10.0f, 10.0f) * moveDistance;
			newZ = Mathf.Min(newZ, GameProperties.top - minDistFromBorder);
			newZ = Mathf.Max(newZ, GameProperties.bottom + minDistFromBorder);

			navAgent.SetDestination(new Vector3(newX, 0, newZ));

		}
	}
}
