using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {
	public float maxSheepPickupDistance;
	public float moveSpeed;
	public GameObject carriedSheep;
	private bool spaceIsHeld = false;
	private bool shouldPickupSheep = false;
	private bool shouldDropSheep = false;

	// Update is called once per frame
	void Update ()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(h, 0.0f, v);
		transform.position += movement * Time.deltaTime * moveSpeed;
		if (h != 0 || v != 0) {
			transform.rotation = Quaternion.LookRotation(movement);
		}
		if (shouldPickupSheep) {
			PickUpClosestSheep ();
			shouldPickupSheep = false;
		} else if (shouldDropSheep) {
			DropSheep ();
			shouldDropSheep = false;
		}
	}

	void FixedUpdate()
	{
		if (Input.GetButtonDown ("Jump")) {
			if (!spaceIsHeld) {
				if (carriedSheep == null) {
					shouldPickupSheep = true;
				} else {
					shouldDropSheep = true;
				}
			}
			spaceIsHeld = true;
		} else {
			spaceIsHeld = false;
		}
	}

	GameObject FindClosestGameObjectWithTag(string tag)
	{
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

	GameObject PickUpClosestSheep()
	{
		if (this.carriedSheep == null)
		{
			GameObject sheep = FindClosestGameObjectWithTag ("Sheep");
			float distance = (this.transform.position - sheep.transform.position).sqrMagnitude;
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
			carriedSheep.transform.position = transform.position;
			carriedSheep.SetActive (true);
			carriedSheep = null;
		}
	}
}
