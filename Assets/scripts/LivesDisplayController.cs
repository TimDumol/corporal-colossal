using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LivesDisplayController : MonoBehaviour {
	public Image lifeDisplay;
	private GameObject levelCanvas;
	private List<Image> lifeDisplays;
	private const int LIFE_PADDING = 10;
	void Awake () {
		StateController.OnLifeChange += UpdateLivesDisplay;
		lifeDisplays = new List<Image>();
	}

	// Use this for initialization
	void Start () {
		levelCanvas = GameObject.Find ("LevelCanvas");
	}

	void UpdateLivesDisplay(int lives) {
		Debug.Log (string.Format ("The lives is {0}", lives));
		if (lifeDisplays.Count > lives) {
			for (int i = lives; i < lifeDisplays.Count; ++i) {
				Debug.Log ("destroying shit");
				Destroy (lifeDisplays[i]);
			}
			lifeDisplays.RemoveRange (lives, lifeDisplays.Count - lives);
		} else if (lifeDisplays.Count < lives) {
			for (int i = lifeDisplays.Count; i < lives; ++i) {
				Debug.Log (string.Format ("Creating shit"));
				Image instance = Instantiate (lifeDisplay) as Image;
				instance.transform.SetParent (levelCanvas.transform, false);
				var pos = instance.rectTransform.anchoredPosition;
				pos.x -= (instance.rectTransform.rect.width + LIFE_PADDING) * i;
				instance.rectTransform.anchoredPosition = pos;
				lifeDisplays.Add (instance);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
