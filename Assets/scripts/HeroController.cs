using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {
	public float maxSheepPickupDistance;
	public float maxEntranceDistance;
	public float moveSpeed;
	public GameObject carriedSheep;
	// space was pressed before last fixedupdate
	private bool spaceIsHeld = false;
	private Animator animator;

	void Start ()
	{
		animator = transform.FindChild ("Player Renderer").GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(h, 0.0f, v);
		transform.position += movement * Time.deltaTime * moveSpeed;
		transform.localScale = SheepMath.GetLocalScale(this.gameObject, h);

		if (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0) {
			animator.SetBool ("Walking", true);
		} else {
			animator.SetBool ("Walking", false);
		}
		if (spaceIsHeld) {
			// Do nothing because space was just being held down.
			if (carriedSheep) {
				DropSheep ();
			} else {
				PickUpClosestSheep ();
			}
		}
		spaceIsHeld = false;
	}

	void Update() {
		if (Input.GetButtonDown ("Jump")) {
			spaceIsHeld = true;
		}
		SheepMath.TranslateFor2D5View(this.gameObject);
	}

	GameObject PickUpClosestSheep()
	{
		if (this.carriedSheep == null)
		{
			GameObject sheep = SheepMath.FindClosestUnsafeSheep (this.gameObject);
			float distance = SheepMath.DistanceBetween2DGameObjects(this.gameObject, sheep.gameObject);
			if (distance < maxSheepPickupDistance) {
				sheep.SetActive (false);
				carriedSheep = sheep;
				animator.SetBool ("CarryingSheep", true);
				return carriedSheep;
			}
		}
		return null;
	}

	void DropSheep()
	{
		if (carriedSheep != null)
		{
			GameObject entrance = SheepMath.FindClosestGameObjectWithTag(this.gameObject, "Pen Entrance");
			float distance = SheepMath.DistanceBetween2DGameObjects(this.gameObject, entrance.gameObject);
			if (distance < maxEntranceDistance) {
				GameObject tmp = carriedSheep;
				carriedSheep = null;
				StateController.AddSheepSaved(tmp);
				animator.SetBool ("CarryingSheep", false);
			} else {
				Renderer renderer = GetComponentInChildren<Renderer>();
				Vector3 dPos = new Vector3(
					renderer.bounds.extents.x * Mathf.Sign(transform.position.x),
					0f,
					renderer.bounds.extents.z * Mathf.Sign(transform.position.z));
				carriedSheep.transform.position = transform.position + dPos;
				carriedSheep.rigidbody.velocity = Vector3.zero;
				carriedSheep.SetActive (true);
				carriedSheep = null;
				animator.SetBool ("CarryingSheep", false);
			}
		}
	}
}
