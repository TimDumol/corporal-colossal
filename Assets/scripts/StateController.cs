using UnityEngine;
using System.Collections;

public class StateController : MonoBehaviour {

	private static GameObject player;

	public delegate void PreLevelStartAction();
	public static event PreLevelStartAction PreLevelStart;
	public delegate void LevelStartAction(int level);
	public static event LevelStartAction OnLevelStart;
	public delegate void LifeChangeAction (int lives);
	public static event LifeChangeAction OnLifeChange;
	public delegate void ScoreChangeAction (int score);
	public static event ScoreChangeAction OnScoreChange;

	public static int level;
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

	public static void addSheepEaten(GameObject sheep) {
		Destroy (sheep);
		if (_lives > 0) {
			_lives -= 1;
		}
		OnLifeChange (lives);
		sheep.GetComponent<SheepController>().safe = true;
		StateController.CheckLevelOver ();
	}

	public static void addPlayerDeath(GameObject player) {
		_lives -= 1;
		OnLifeChange (lives);
	}

	public static void AddSheepSaved(GameObject sheep) {
		_score += 1;
		Debug.Log ("Sheep saved");
		SheepFencerController.FenceSheep (sheep);
		StateController.OnSheepSaved (sheep);
		OnScoreChange(_score);
	}

	void Awake () {
		Random.seed = System.Environment.TickCount;

		PreLevelStart += () => {};
		OnLevelStart += (int level) => {};
		OnLifeChange += (int lives) => {};
		OnScoreChange += (int score) => {};
	}

	public static void ResetLives() {
		_lives = GameProperties.initialLives;
		OnLifeChange (lives);
	}

	// Use this for initialization
	void Start () {
		StateController.level = 1;
		PreLevelStart ();
		ResetLives ();
		OnLevelStart (StateController.level);
		player = GameObject.FindGameObjectWithTag ("Player");
    }

	public static int CountUnsafeSheep () {
		int unsafeSheep = 0;
		GameObject[] sheeps = GameObject.FindGameObjectsWithTag ("Sheep");
		foreach (GameObject s in sheeps) {
			if (!s.GetComponent<SheepController>().safe) {
				unsafeSheep += 1;
			}
		}
		if (player.GetComponent<HeroController> ().carriedSheep) {
			unsafeSheep += 1;
		}
		return unsafeSheep;
	}

	static void CheckLevelOver() {
		int unsafeSheep = StateController.CountUnsafeSheep ();
		if (unsafeSheep == 0) {
			ClearLevel();
			StateController.level += 1;
			if (StateController.OnLevelStart != null) {
				StateController.OnLevelStart(StateController.level);
			}
		}
	}

	static void OnSheepSaved(GameObject sheep) {
		StateController.CheckLevelOver ();
	}

	static void DestroyAllGameObjectsWithTag(string tag) {
		GameObject[] objs = GameObject.FindGameObjectsWithTag (tag);
		foreach (GameObject o in objs) {
			Destroy (o);
		}
	}

	static void ClearLevel() {
		DestroyAllGameObjectsWithTag ("Player");
		DestroyAllGameObjectsWithTag ("Sheep");
		DestroyAllGameObjectsWithTag ("Enemy");
	}

	void OnLifeChanged (int lives) {
		if (lives <= 0) {
			StateController.ClearLevel();
			Debug.Log ("Game Over!");
			// TODO: Go to game over screen.
		}
	}

	void OnLevelWasLoaded(int levelId) {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
