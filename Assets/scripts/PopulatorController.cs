﻿using UnityEngine;
using System.Collections;

public class PopulatorController : MonoBehaviour
{
	public GameObject sheep;
	private Vector3 sheepSize;

	void Awake ()
	{
			StateController.OnGameStart += OnGameStart;
	}
	// Use this for initialization
	void Start ()
	{
		sheepSize = sheep.transform.localScale;
		//Debug.Log (string.Format ("my size is : {0}; {1}", sheepSize, sheep.collider.bounds));
	}

	void SpawnSheep ()
	{
		bool spawned = false;
		do {
			float z = Random.Range (GameProperties.bottom, GameProperties.top);
			float x = Random.Range (GameProperties.left, GameProperties.right);
			Debug.Log (string.Format ("Generated: {0}, {1}; sheep size is: {2} ", x, z, sheepSize));
			if (Physics.CheckCapsule (new Vector3(x - sheepSize.x/2f, z/2f + 0.5f, z), new Vector3(x + sheepSize.x/2f, z/2f + 0.5f, z), z/2f)) {
				Debug.Log (string.Format ("Pass: {0}, {1}; sheep size is: {2} ", x, z, sheepSize));
				Instantiate (sheep, new Vector3 (x, 1, z), Quaternion.identity);
				spawned = true;
			}

		} while (!spawned);
	}

	void OnGameStart ()
	{
		for (int i = 0; i < 10; ++i) {
						SpawnSheep ();

				}
		Instantiate (sheep, new Vector3 (50, 1, 50), Quaternion.identity);
	}

	// Update is called once per frame
	void Update ()
	{

	}
}
