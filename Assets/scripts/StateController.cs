using UnityEngine;
using System.Collections;

public class StateController : MonoBehaviour {
	
	public delegate void GameStartAction();
	public static event GameStartAction OnLevelStart;
	public delegate void LifeChangeAction (int lives);
	public static event LifeChangeAction OnLifeChange;
	private static int _lives;
	public static int lives {
		get {
			return _lives;
		}
	}

	public static void addSheepEaten() {
		_lives -= 1;
		OnLifeChange (lives);
	}

	void Awake () {
		Random.seed = System.Environment.TickCount; 
	}

	void ResetLives() {
		_lives = GameProperties.initialLives;
		OnLifeChange (lives);
	}

	// Use this for initialization
	void Start () {
		ResetLives ();
		if (OnLevelStart != null) {
				OnLevelStart ();
		}
    }
    
    void OnLevelWasLoaded(int levelId) {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
