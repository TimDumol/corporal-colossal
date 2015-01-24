using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	private NavMeshAgent navAgent;

	// Use this for initialization
	void Start () {
		navAgent = this.GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update () {
		MoveTowardsClosestSheep ();
	}

	void UtilizeSheep(GameObject sheep){
		Destroy (sheep);
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
			Vector3 v = this.transform.position - obj.transform.position;
			if (v.sqrMagnitude < distance) {
				closest = obj;
				distance = v.sqrMagnitude;
			}
		}
		
		return closest;
	}

	void OnCollisionEnter(Collision collision) {
		Collider other = collision.collider;
		if (other.gameObject.tag == "Sheep") {
			UtilizeSheep (other.gameObject);
		}
	}
}
