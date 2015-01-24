using UnityEngine;
using System.Collections;

public class StateController : MonoBehaviour {

	public delegate void PreLevelStartAction();
	public static event PreLevelStartAction PreLevelStart;
	public delegate void LevelStartAction();
	public static event LevelStartAction OnLevelStart;
	public delegate void LifeChangeAction (int lives);
	public static event LifeChangeAction OnLifeChange;
	public delegate void ScoreChangeAction (int score);
	public static event ScoreChangeAction OnScoreChange;
	public delegate void SheepSaveAction (GameObject sheep);
	public static event SheepSaveAction OnSheepSave;

	private static int _lives;
	public static int lives {
		get {
			return _lives;
		}
	}

	private static int _score;
	public static int score {
		get {
			return _score;
		}
	}

	public static void addSheepEaten() {
				_lives -= 1;
				OnLifeChange (lives);
	}

	public static void AddSheepSaved(GameObject sheep) {
		_score += 1;
		OnScoreChange(_score);
		OnSheepSave(sheep);
	}

	void Awake () {
		Random.seed = System.Environment.TickCount;

		PreLevelStart += () => {};
		OnLevelStart += () => {};
		OnLifeChange += (int lives) => {};
		OnScoreChange += (int score) => {};
		OnSheepSave += (GameObject sheep) => {};
	}

	void ResetLives() {
		_lives = GameProperties.initialLives;
		OnLifeChange (lives);
	}

	// Use this for initialization
	void Start () {
		PreLevelStart ();
		ResetLives ();
		OnLevelStart ();
    }
    
    void OnLevelWasLoaded(int levelId) {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
