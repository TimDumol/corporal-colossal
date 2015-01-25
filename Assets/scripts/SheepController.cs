using UnityEngine;
using System.Collections;

public class SheepController : MonoBehaviour
{
	public NavMeshAgent navAgent;

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
			navAgent.SetDestination(SheepMath.GetRandomPlaceToGoTo(
				this.gameObject,
				GameProperties.topPadded,
				GameProperties.rightPadded,
				GameProperties.bottomPadded,
				GameProperties.leftPadded));
		}
		SheepMath.TranslateFor2D5View(this.gameObject);
	}
}
