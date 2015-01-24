using UnityEngine;
using System.Collections.Generic;

public class HighscoreController : MonoBehaviour {
	public static SortedDictionary<string, int> highscoreDict;

	void Awake () {
		string serializedHighscores = PlayerPrefs.GetString ();
		foreach (string strTuple in serializedHighscores.Split (";")) {
			string[] splitted = strTuple.Split (",");
			highscoreDict[splitted[0]] = int.Parse(splitted[1]);
		}
	}
}
