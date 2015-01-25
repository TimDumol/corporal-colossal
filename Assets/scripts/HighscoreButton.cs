using UnityEngine;
using System.Collections;

public class HighscoreButton : MonoBehaviour {

	void OnMouseDown()
	{
		Application.LoadLevel(4);
		Time.timeScale = 0;
	}
}
