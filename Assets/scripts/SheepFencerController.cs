using UnityEngine;
using System.Collections;

public class SheepFencerController : MonoBehaviour {

	void Awake () {
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	public static void FenceSheep(GameObject sheep) {
		Debug.Log ("Sheep saved");
		sheep.GetComponent<SheepController>().safe = true;
		var pos = GameObject.Find ("Sheep Pen").transform.position;
		pos.y = sheep.transform.position.y;
		sheep.transform.position = pos;
		sheep.SetActive(true);
	}
}
