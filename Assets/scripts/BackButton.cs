using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour {

	void OnMouseDown()
	{
		Application.LoadLevel(0);
		Time.timeScale = 0;
	}
}
