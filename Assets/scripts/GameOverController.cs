using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverController : MonoBehaviour {
	private static GameObject levelCanvas;
	public Text namePseudofield;
	public Text EndScoreText;
	public Image gameOverImage;
	public Button restartButton;
	public Button titleButton;
	public Button addScoreButton;
	public Image EndScoreRibbon;
	public Image NameInputRibbon;
	public GameObject buttonQuad;
	private GameObject titleQuadInstance;
	private GameObject restartQuadInstance;
	private GameObject addScoreQuadInstance;
	private Text fieldInstance;
	private bool gameOver = false;
	private bool typedSomething = false;
	void Awake () {

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
			if (Input.GetKeyDown ("backspace")) {
				if (fieldInstance.text.Length > 0) {
					fieldInstance.text.Substring (0, fieldInstance.text.Length - 1);
				}
			}
			if (Input.GetKeyDown("return") || Input.GetKeyDown("enter")) {
				Restart ();
			}
			if (Input.GetKeyDown ("tab")) {
				GoToTitle();
			}
			if (Input.GetKeyDown ("left ctrl")) {
				GoToHighscore();
			}
		}
	}

	public void ShowGameOver(int score) {
		levelCanvas = GameObject.FindGameObjectWithTag("LevelCanvas");
		Image instance = Instantiate (gameOverImage) as Image;
		instance.transform.SetParent (levelCanvas.transform, false);

		Button instance2 = Instantiate (restartButton) as Button;
		instance2.transform.SetParent (levelCanvas.transform, false);
		instance2.onClick.AddListener (() => {Restart();});

		Button instance3 = Instantiate (addScoreButton) as Button;
		instance3.transform.SetParent (levelCanvas.transform, false);
		instance3.onClick.AddListener(() => {GoToHighscore();});

		Button instance4 = Instantiate (titleButton) as Button;
		instance4.transform.SetParent (levelCanvas.transform, false);

		Image instance5 = Instantiate (EndScoreRibbon) as Image;
		instance5.transform.SetParent (levelCanvas.transform, false);

		Image instance6 = Instantiate (NameInputRibbon) as Image;
		instance6.transform.SetParent (levelCanvas.transform, false);

		Text instance7 = Instantiate (EndScoreText) as Text;
		instance7.transform.SetParent (levelCanvas.transform, false);
		instance7.text = "YOUR SCORE: " + score;

		fieldInstance = Instantiate (namePseudofield) as Text;
		fieldInstance.transform.SetParent (levelCanvas.transform, false);
		var pos = fieldInstance.rectTransform.anchoredPosition;
		pos.y = -100;
		fieldInstance.rectTransform.anchoredPosition = pos;
		fieldInstance.text = "INPUT NAME";

		gameOver = true;
		typedSomething = false;
    }

	public static void GoToTitle () {
		//Destroy (GameObject.Find("StateController"));
		Application.LoadLevel (0);
    }

	public static void Restart() {
		//GameObject.Find ("StateController").GetComponent <StateController> ().Init ();
		Application.LoadLevel (5);
    }

	public  static void GoToHighscore () {
		Application.LoadLevel (4);
	}
}
