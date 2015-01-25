using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverController : MonoBehaviour {
	private static GameObject levelCanvas;
	public Text namePseudofield;
	public Image gameOverImage;
	private Text fieldInstance;
	private bool gameOver = false;
	private bool typedSomething = false;
	void Awake () {
		StateController.OnEndGame += ShowGameOver;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gameOver) {
			char? keyTyped = null;
			for (int i = 0; i < 26; ++i) {
				string str = ((char)('a' + i)).ToString ();
				if (Input.GetKeyDown (str)) {
					keyTyped = (char)('a' + i);
				}
			}
			if (Input.GetKeyDown ("space")) {
				keyTyped = ' ';
			}
			if (keyTyped.HasValue) {
				if (!typedSomething) {
					fieldInstance.text = "";
				}
				typedSomething = true;
				fieldInstance.text = fieldInstance.text + char.ToUpper (keyTyped.Value);
			}
			if (Input.GetKeyDown ("enter")) {
				GoToHighscore();
			}
		}
	}

	public void ShowGameOver(int score) {
		levelCanvas = GameObject.FindGameObjectWithTag("LevelCanvas");
		Image instance = Instantiate (gameOverImage) as Image;
		instance.transform.SetParent (levelCanvas.transform, false);

		fieldInstance = Instantiate (namePseudofield) as Text;
		fieldInstance.transform.SetParent (levelCanvas.transform, false);
		var pos = fieldInstance.rectTransform.anchoredPosition;
		pos.y -= 100;
		fieldInstance.rectTransform.anchoredPosition = pos;
		fieldInstance.text = "INPUT NAME";

		gameOver = true;
		typedSomething = false;
    }

	public void GoToHighscore () {
		Application.LoadLevel (4);
	}
}
