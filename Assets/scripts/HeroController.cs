using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {
	public float maxSheepPickupDistance;
	public float maxEntranceDistance;
	public float moveSpeed;
	public GameObject carriedSheep;
	// space was pressed before last fixedupdate
	private bool spaceIsHeld = false;
	private bool shouldPickupSheep = false;
	private bool shouldDropSheep = false;

	// Update is called once per frame
	void FixedUpdate ()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(h, 0.0f, v);
		transform.position += movement * Time.deltaTime * moveSpeed;
		if (h != 0 || v != 0) {
			transform.rotation = Quaternion.LookRotation(movement);
		}
		if (spaceIsHeld) {
			if (carriedSheep) {
				DropSheep();
			} else {
				PickUpClosestSheep();
			}
		}
		spaceIsHeld = false;
	}

	void Update()
	{
		if (Input.GetButtonDown ("Jump")) {
			spaceIsHeld = true;
		}
	}

	GameObject FindClosestGameObjectWithTag(string tag)
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag (tag);
		float distance = float.MaxValue;
		GameObject closest = null;
		
		foreach(GameObject obj in objs) {
			Vector3 v = this.transform.position - obj.transform.position;
			float sqr = SheepMath.SqrMagnitude2D(v);
			if (sqr < distance) {
				closest = obj;
				distance = sqr;
			}
		}
		
		return closest;
	}

	GameObject PickUpClosestSheep()
	{
		if (this.carriedSheep == null)
		{
			GameObject sheep = FindClosestGameObjectWithTag ("Sheep");
			Debug.Log (string.Format ("My closest sheep is here: {0}; I am here! {1}", sheep.transform.position, this.transform.position));
			float distance = SheepMath.SqrMagnitude2D(this.transform.position - sheep.transform.position);
			if (distance < maxSheepPickupDistance) {
				sheep.SetActive (false);
				carriedSheep = sheep;
				return carriedSheep;
			}
		}
		return null;
	}

	void DropSheep()
	{
		if (carriedSheep != null)
		{
			GameObject entrance = FindClosestGameObjectWithTag("Pen Entrance");
			float distance = SheepMath.SqrMagnitude2D(this.transform.position - entrance.transform.position);
			Debug.Log ("distance " + distance + " " + maxEntranceDistance);
			if (distance < maxEntranceDistance) {
				StateController.AddSheepSaved(carriedSheep);
				carriedSheep = null;
			} else {
				Vector3 dPos = new Vector3(
					renderer.bounds.extents.x * Mathf.Sign(transform.position.x),
					0f,
					renderer.bounds.extents.z * Mathf.Sign(transform.position.z));
				carriedSheep.transform.position = transform.position + dPos;
				carriedSheep.SetActive (true);
				carriedSheep = null;
			}
		}
	}
}
