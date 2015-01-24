using UnityEngine;
using System.Collections;

public class ResolutionController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.SetResolution (800, 800, false);
	}

	int lastWidth = Screen.width;
	void Update () {
		if (Camera.main.aspect != 1) {
			if (Screen.width != lastWidth) {
				// user is resizing width
				Screen.SetResolution(Screen.width, Screen.width, false);
				lastWidth = Screen.width;
			} else {
				// user is resizing height
				Screen.SetResolution(Screen.height, Screen.height, false);
			}
		}
	}
}


