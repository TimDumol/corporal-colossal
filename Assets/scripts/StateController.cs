using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StateController : MonoBehaviour {

	private static GameObject player;

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
		GameObject.Find ("StateController").GetComponent<StateController>().OnLifeChangeSub (lives);

		StateController.CheckLevelOver ();
	}

	public static void addPlayerDeath(GameObject player) {
		_lives -= 1;
		GameObject.Find ("StateController").GetComponent<StateController>().OnLifeChangeSub (lives);

	}

	public static void AddSheepSaved(GameObject sheep) {
		_score += 1;
		SheepFencerController.FenceSheep (sheep);
		StateController.OnSheepSaved (sheep);
		GameObject.Find ("StateController").GetComponent<StateController>().OnScoreChangeSub(_score);
	}

	public void OnLifeChangeSub(int lives) {
		GameObject.Find ("LivesDisplayController").GetComponent<LivesDisplayController> ().UpdateLivesDisplay (lives);
		CheckGameOver (lives);
	}

	void Awake () {
		Random.seed = System.Environment.TickCount;
	}

	public static void ResetLives() {
		_lives = GameProperties.initialLives;
		GameObject.Find ("StateController").GetComponent<StateController>().OnLifeChangeSub (lives);
	}

	// Use this for initialization
	void Start () {
		//StartCoroutine(DelayedInit ());
    }

	public IEnumerator DelayedInit() {
		yield return new WaitForSeconds (1);
		Init ();
	}

	public void OnLevelStartSub(int level) {
		Debug.Log ("SPAWNING AND CRAP");
		GameObject.Find ("PopulatorController").GetComponent<PopulatorController> ().OnLevelStart (level);
		BackgroundMusicController.GetInstance ().StartBleeping (level);
		//GameObject.Find ("BackgroundMusicController").GetComponent<BackgroundMusicController> ().StartBleeping (level);
		GameObject.Find ("Scorebar").GetComponent<ScoreDisplayController> ().ShowScore (level);
	}

	public void OnLevelEndSub(int level) {
		BackgroundMusicController.GetInstance ().StopBleeping (level);
		//GameObject.Find ("BackgroundMusicController").GetComponent<BackgroundMusicController> ().StopBleeping (level);
	}

	public void OnScoreChangeSub(int score) {
		GameObject.Find ("Scorebar").GetComponent<ScoreDisplayController> ().ShowScore (score);
	}

	public void OnLevelWasLoaded(int level) {
		if (level == 1) {
			Debug.Log ("noted");
			StartCoroutine(DelayedInit ());
		}
	}

	public void Init() {
		StateController.level = 1;
		ResetLives ();
		//OnLevelStart (StateController.level);
		OnLevelStartSub (level);
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
			GameObject.Find ("StateController").GetComponent<StateController>().OnEndGameSub(score);
        }
	}

	public void OnEndGameSub(int score) {
		GameObject.Find ("GameOverController").GetComponent<GameOverController> ().ShowGameOver (score);
	}

	static void CheckLevelOver() {
		int unsafeSheep = StateController.CountUnsafeSheep ();
		if (unsafeSheep == 0) {
			ClearLevel();
			GameObject.Find ("StateController").GetComponent<StateController>().OnLevelEndSub(StateController.level);
		
			StateController.level += 1;
           
				GameObject.Find ("StateController").GetComponent<StateController>().OnLevelStartSub(StateController.level);

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
