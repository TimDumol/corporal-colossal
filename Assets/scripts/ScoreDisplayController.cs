using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplayController : MonoBehaviour {

	public Text scoreText;
	private GameObject levelCanvas;
	private Text instance;

	// Use this for initialization
	void Start () {
		ShowScore (0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowScore (int score) {
		levelCanvas = GameObject.FindGameObjectWithTag("LevelCanvas");
		if (instance != null) {
						Destroy (instance);
				}
		instance = Instantiate (scoreText) as Text;
		instance.text = string.Format("SCORE: {0}", score);
		instance.transform.SetParent (levelCanvas.transform, false);
	}
}
