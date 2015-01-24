using UnityEngine;
using System.Collections.Generic;

public class HighscoreController : MonoBehaviour {
	public static SortedDictionary<string, int> highscoreDict;

	void Awake () {
		string serializedHighscores = PlayerPrefs.GetString ("highscores");
		foreach (string strTuple in serializedHighscores.Split (";".ToCharArray())) {
			string[] splitted = strTuple.Split (",".ToCharArray());
			highscoreDict[splitted[0]] = int.Parse(splitted[1]);
		}
	}
}
