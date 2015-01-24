using UnityEngine;
using System.Collections;

public class SheepController : MonoBehaviour
{
	private NavMeshAgent navAgent;

	public float moveSpeed = 5;

	void Start ()
	{
		navAgent = this.GetComponent<NavMeshAgent>();
	}

	void Update ()
	{
		float dx = Random.Range (-10.0f, 10.0f) * moveSpeed;
		float dz = Random.Range (-10.0f, 10.0f) * moveSpeed;

		Vector3 targetPosition = transform.position + new Vector3(dx, 0, dz);
		navAgent.SetDestination(targetPosition);
	}
}
