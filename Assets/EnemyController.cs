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
		GameObject[] sheeps = GameObject.FindGameObjectsWithTag ("Sheep");
		float distance = float.MaxValue;
		GameObject closestSheep = null;

		foreach(GameObject sheep in sheeps) {
			Vector3 v = this.transform.position - sheep.transform.position;
			if (v.sqrMagnitude < distance) {
				closestSheep = sheep;
				distance = v.sqrMagnitude;
			}
		}

		navAgent.SetDestination (closestSheep.transform.position);
		Debug.Log (closestSheep);
	}
}
