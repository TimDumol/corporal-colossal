using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {

	public float moveSpeed;

	// Update is called once per frame
	void Update ()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(h, 0.0f, v);
		transform.position += movement * Time.deltaTime * moveSpeed;
		transform.rotation = Quaternion.LookRotation(movement);
	}
}
