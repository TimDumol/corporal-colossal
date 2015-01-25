using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseController : MonoBehaviour {
	public bool paused = false;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P)){
			paused = !paused;
			if (paused) {
				Pause ();
			} else {
				Unpause ();
			}
		}
	}

	void Pause() {
		Time.timeScale = 0;
	}

	void Unpause(){
		Time.timeScale = 1;
	}
}
