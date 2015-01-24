using UnityEngine;
using System.Collections;

public class InstructionsButton : MonoBehaviour {
	
	void OnMouseDown()
	{
		Application.LoadLevel(2);
		Time.timeScale = 0;
	}
}
