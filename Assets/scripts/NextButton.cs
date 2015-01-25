using UnityEngine;
using System.Collections;

public class NextButton : MonoBehaviour {

	void OnMouseDown()
	{
		Application.LoadLevel(1);
		Time.timeScale = 1;
	}
}
