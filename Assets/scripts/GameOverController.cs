using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverController : MonoBehaviour {
	private static GameObject levelCanvas;
	public Image gameOverImage;
	void Awake () {
		StateController.OnEndGame += ShowGameOver;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowGameOver(int score) {
		levelCanvas = GameObject.FindGameObjectWithTag("LevelCanvas");
		Image instance = Instantiate (gameOverImage) as Image;
		instance.transform.SetParent (levelCanvas.transform, false);
	}
}
