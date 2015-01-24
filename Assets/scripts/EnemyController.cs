using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	private NavMeshAgent navAgent;

	// Use this for initialization
	void Start () {
		navAgent = this.GetComponent<NavMeshAgent>();
		navAgent.updateRotation = false;
	}

	void FixedUpdate () {
		MoveTowardsClosestSheep ();
		Vector3 scale = SheepMath.GetLocalScale (this.gameObject, rigidbody.velocity.x + navAgent.velocity.x);
		transform.localScale = scale;
	}

	void UtilizeSheep(GameObject sheep){
		StateController.addSheepEaten (sheep);
	}

	void KillPlayer(GameObject player) {
		StateController.addPlayerDeath (player);
	}

	void MoveTowardsClosestSheep() {
		GameObject sheep = SheepMath.FindClosestUnsafeSheep (this.gameObject);
		if (sheep != null) {
			navAgent.SetDestination (sheep.transform.position);
		}
	}

	void OnCollisionEnter(Collision collision) {
		Collider other = collision.collider;
		if (other.gameObject.tag == "Sheep" && !other.gameObject.GetComponent<SheepController>().safe) {
			Debug.Log (this);
			Debug.Log ("sheep collision la");
			UtilizeSheep (other.gameObject);
		}
	}
}

