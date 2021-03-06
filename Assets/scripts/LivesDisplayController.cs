﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LivesDisplayController : MonoBehaviour {
	public Image lifeDisplay;
	private GameObject levelCanvas;
	private List<Image> lifeDisplays;
	private const int LIFE_PADDING = 2;
	void Awake () {

		lifeDisplays = new List<Image>();
	}

	// Use this for initialization
	void Start () {
	}
	

	public void UpdateLivesDisplay(int lives) {
		levelCanvas = GameObject.FindGameObjectWithTag("LevelCanvas");
		if (lifeDisplays.Count > lives) {
			for (int i = lives; i < lifeDisplays.Count; ++i) {
				Destroy (lifeDisplays[i]);
			}
			lifeDisplays.RemoveRange (lives, lifeDisplays.Count - lives);
		} else if (lifeDisplays.Count < lives) {
			for (int i = lifeDisplays.Count; i < lives; ++i) {
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
