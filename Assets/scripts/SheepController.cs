using UnityEngine;
using System.Collections;

public class SheepController : MonoBehaviour
{
	public NavMeshAgent navAgent;

	public float moveDistance = 1;
	public float minDistFromBorder = 10;

	public bool safe; 

	void Start ()
	{
		navAgent = this.GetComponent<NavMeshAgent>();
		navAgent.updateRotation = false;
		safe = false;
	}

	void FixedUpdate()
	{
		Vector3 scale = SheepMath.GetLocalScale (this.gameObject, rigidbody.velocity.x + navAgent.velocity.x);
		transform.localScale = scale;
	}

	void Update ()
	{
		if (safe && navAgent.hasPath) {
			navAgent.Stop();
		} else if (navAgent.remainingDistance < 1) {
			float newX = transform.position.x + Random.Range(-10.0f, 10.0f) * moveDistance;
			newX = Mathf.Min(newX, GameProperties.right - minDistFromBorder);
			newX = Mathf.Max(newX, GameProperties.left + minDistFromBorder);

			float newZ = transform.position.z + Random.Range(-10.0f, 10.0f) * moveDistance;
			newZ = Mathf.Min(newZ, GameProperties.top - minDistFromBorder);
			newZ = Mathf.Max(newZ, GameProperties.bottom + minDistFromBorder);

			navAgent.SetDestination(new Vector3(newX, 0, newZ));
		}
		SheepMath.TranslateFor2D5View(this.gameObject);
	}
}
