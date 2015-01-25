using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplayController : MonoBehaviour {

	public Text scoreText;
	private GameObject levelCanvas;

	// Use this for initialization
	void Start () {
		StateController.PreLevelStart += FindLevelCanvas;
		StateController.OnLevelStart += ShowScore;
		StateController.OnScoreChange += ShowScore;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FindLevelCanvas() {
		levelCanvas = GameObject.FindGameObjectWithTag("LevelCanvas");
	}

	void ShowScore (int score) {
		Text instance = Instantiate (scoreText) as Text;
		instance.text = string.Format("Score: {0}", score);
		instance.transform.SetParent (levelCanvas.transform, false);
	}
}
