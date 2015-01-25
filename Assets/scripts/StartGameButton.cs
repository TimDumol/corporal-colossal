using UnityEngine;
using System.Collections;

public class StartGameButton : MonoBehaviour {

	void OnMouseDown()
	{
		Application.LoadLevel(5);
		Time.timeScale = 0;
	}
}
