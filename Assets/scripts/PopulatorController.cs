using UnityEngine;
using System.Collections;

public class PopulatorController : MonoBehaviour {

	void Awake () {
		StateController.OnGameStart += OnGameStart;
	}
	// Use this for initialization
	void Start () {

	}

	void OnGameStart() {
		Debug.Log ("Game has started");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
