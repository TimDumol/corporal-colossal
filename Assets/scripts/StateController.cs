using UnityEngine;
using UnityEngine.UI;
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
	public delegate void LevelEndAction(int level);
	public static event LevelEndAction OnLevelEnd;
	public delegate void EndGameAction(int score);
	public static event EndGameAction OnEndGame;

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
		if (_lives > 0) {
			_lives -= 1;
		}
		OnLifeChange (lives);

		StateController.CheckLevelOver ();
	}

	public static void addPlayerDeath(GameObject player) {
		_lives -= 1;
		OnLifeChange (lives);

	}

	public static void AddSheepSaved(GameObject sheep) {
		_score += 1;
		SheepFencerController.FenceSheep (sheep);
		StateController.OnSheepSaved (sheep);
		OnScoreChange(_score);
	}

	void Awake () {
		Random.seed = System.Environment.TickCount;

		PreLevelStart += () => {};
		OnLevelStart += (int level) => {};
		OnLifeChange += (int lives) => {};
		OnLifeChange += CheckGameOver;
		OnScoreChange += (int score) => {};
		OnEndGame += (int score) => {};
		OnLevelEnd += (int level) => {};
	}

	public static void ResetLives() {
		_lives = GameProperties.initialLives;
		OnLifeChange (lives);
	}

	// Use this for initialization
	void Start () {
		Init ();
    }

	public void Init() {
		StateController.level = 1;
		PreLevelStart ();
		ResetLives ();
		OnLevelStart (StateController.level);

	}

	public static int CountUnsafeSheep () {
		int unsafeSheep = 0;
		GameObject[] sheeps = GameObject.FindGameObjectsWithTag ("Sheep");
		foreach (GameObject s in sheeps) {
			if (!s.GetComponent<SheepController>().safe) {
				unsafeSheep += 1;
			}
		}
		player = GameObject.FindGameObjectWithTag ("Player");
		if (player.GetComponent<HeroController> ().carriedSheep) {
			unsafeSheep += 1;
		}
		return unsafeSheep;
	}

	static void CheckGameOver(int lives) {
		if (lives <= 0) {
			// Game over.
			Time.timeScale = 0;
			OnEndGame(score);
        }
	}

	static void CheckLevelOver() {
		int unsafeSheep = StateController.CountUnsafeSheep ();
		if (unsafeSheep == 0) {
			ClearLevel();
			OnLevelEnd(StateController.level);
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
		DestroyAllGameObjectsWithTag ("Sheep");
		DestroyAllGameObjectsWithTag ("Enemy");
		DestroyAllGameObjectsWithTag ("Player");
	}

	void OnLifeChanged (int lives) {
		if (lives <= 0) {
			StateController.ClearLevel();
			Debug.Log ("Game Over!");
			// TODO: Go to game over screen.
		}
	}
}
