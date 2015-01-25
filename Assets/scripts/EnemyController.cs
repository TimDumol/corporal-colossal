using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	private NavMeshAgent navAgent;
	private Animator animator;
	private float speed;

	// Use this for initialization
	void Start () {
		navAgent = this.GetComponent<NavMeshAgent>();
		navAgent.updateRotation = false;
		animator = transform.GetComponentInChildren<Animator>();
	}

	void FixedUpdate () {
		if (!animator.GetBool ("Loving")) {
			MoveTowardsClosestSheep ();
			Vector3 scale = SheepMath.GetLocalScale (this.gameObject, rigidbody.velocity.x + navAgent.velocity.x);
			transform.localScale = scale;
		}
	}

	void Update () {
		SheepMath.TranslateFor2D5View(this.gameObject);
	}

	void EliminateSheep(GameObject sheep) {
		sheep.GetComponent<SheepController>().safe = true;
		Destroy (sheep);
	}

	IEnumerator UtilizeSheep(GameObject sheep){
		speed = navAgent.speed;
		navAgent.speed = 0;
		rigidbody.velocity = navAgent.velocity = Vector3.zero;
		animator.SetBool ("Loving", true);
		EliminateSheep (sheep);
		yield return new WaitForSeconds(1.0f);

		StateController.addSheepEaten (sheep);
		animator.SetBool ("Loving", false);
		navAgent.speed = speed;
	}

	void KillPlayer(GameObject player) {
		StateController.addPlayerDeath (player);
	}

	void MoveTowardsClosestSheep() {
		GameObject sheep = SheepMath.FindClosestUnsafeSheep (this.gameObject);
		if (sheep != null) {
			navAgent.SetDestination (sheep.transform.position);
		} else {
			navAgent.SetDestination(SheepMath.GetRandomPlaceToGoTo(
				this.gameObject,
				GameProperties.topPadded,
				GameProperties.rightPadded,
				GameProperties.bottomPadded,
				GameProperties.leftPadded));
		}
//		DrawPath(navAgent.path); for debugging NavMeshPath
	}

	void DrawPath (NavMeshPath path) {
		if (path.corners.Length < 2)
			return;
		Color c = Color.black;
		switch(path.status) {
		case NavMeshPathStatus.PathComplete:
			c = Color.white;
			break;
		case NavMeshPathStatus.PathInvalid:
			c = Color.red;
			break;
		case NavMeshPathStatus.PathPartial:
			c = Color.yellow;
			break;
		}
		
		Vector3 previousCorner = path.corners[0];
		
		int i = 1;
		while (i < path.corners.Length) {
			Vector3 currentCorner = path.corners[i];
			Debug.DrawLine(previousCorner, currentCorner, c);
			previousCorner = currentCorner;
			i++;
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		Collider other = collision.collider;
		if (!animator.GetBool ("loving") && other.gameObject.tag == "Sheep" && !other.gameObject.GetComponent<SheepController>().safe) {
			Debug.Log (this);
			StartCoroutine(UtilizeSheep (other.gameObject));
		}
	}
}

