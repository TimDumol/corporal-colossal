using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	private NavMeshAgent navAgent;

	// Use this for initialization
	void Start () {
		navAgent = this.GetComponent<NavMeshAgent>();
		navAgent.updateRotation = false;
	}

	// Update is called once per frame
	void Update () {
		MoveTowardsClosestSheep ();
	}

	void UtilizeSheep(GameObject sheep){
		StateController.addSheepEaten (sheep);
	}

	void KillPlayer(GameObject player) {
		StateController.addPlayerDeath (player);
	}

	void MoveTowardsClosestSheep() {
		GameObject sheep = FindClosestGameObjectWithTag ("Sheep");
		if (sheep != null) {
			navAgent.SetDestination (sheep.transform.position);
		}
	}

	GameObject FindClosestGameObjectWithTag(string tag) {
		GameObject[] objs = GameObject.FindGameObjectsWithTag (tag);
		float distance = float.MaxValue;
		GameObject closest = null;

		foreach(GameObject obj in objs) {
			if (obj.GetComponent<SheepController>().safe)
				continue;
			Vector3 v = this.transform.position - obj.transform.position;
			float sqr = SheepMath.SqrMagnitude2D(v);
			if (sqr < distance) {
				closest = obj;
				distance = sqr;
			}
		}
		
		return closest;
	}

	void OnCollisionEnter(Collision collision) {
		Collider other = collision.collider;
		if (other.gameObject.tag == "Sheep" && !other.gameObject.GetComponent<SheepController>().safe) {
			UtilizeSheep (other.gameObject);
		} else if (other.gameObject.tag == "Player") {
			KillPlayer (other.gameObject);
		}
	}
}

