﻿using UnityEngine;
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
		sheep.transform.position = new Vector3(0, 1, 0);
		sheep.SetActive(true);
	}
}
