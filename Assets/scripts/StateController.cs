using UnityEngine;
using System.Collections;

public class StateController : MonoBehaviour {
	
	public delegate void GameStartAction();
	public static event GameStartAction OnGameStart;
	private static int _score;

	public static int score {
		get {
			return _score;
		}
	}

	public static void AddSheepSaved(GameObject sheep) {
		_score += 1;
		OnSheepSaved(sheep);
	}

	public static void OnSheepSaved(GameObject sheep) {
		Debug.Log ("Sheep saved");
	}

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
