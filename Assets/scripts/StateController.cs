using UnityEngine;
using System.Collections;

public class StateController : MonoBehaviour {
	
	public delegate void GameStartAction();
	public static event GameStartAction OnGameStart;

	void Awake () {
		Random.seed = System.Environment.TickCount; 
	}

	// Use this for initialization
	void Start () {
		if (OnGameStart != null) {
			OnGameStart ();
		}
	}

	void OnLevelWasLoaded(int levelId) {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
