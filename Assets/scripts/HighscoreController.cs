using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HighscoreController : MonoBehaviour {
	public static SortedDictionary<string, int> highscoreDict;
	public GameObject highScoreName;
	public GameObject highScoreScore;
	public static List<TextTuple> highScoreList;

	public class TextTuple {
		public string name;
		public int score;

		public TextTuple(string rname, int rscore) {
			this.name = rname;
			this.score = rscore;
		}
	}
	
	public static int TextTupleComparator(TextTuple a, TextTuple b) {
		if (a.score != b.score) {
			return b.score - a.score;
		}
		return a.name.CompareTo (b.name);
	}

	void Awake () {
		/*
		string serializedHighscores = PlayerPrefs.GetString ("highscores");
		foreach (string strTuple in serializedHighscores.Split (";".ToCharArray())) {
			string[] splitted = strTuple.Split (",".ToCharArray());
			highscoreDict[splitted[0]] = int.Parse(splitted[1]);
		}
		*/
		highScoreList = new List<TextTuple>();

		addHighScore ("Jack1", 1);
		addHighScore ("Jack12", 12);
		addHighScore ("Jack11", 11);
		addHighScore ("Jack23", 23);
		renderScores ();
	}

	void PurgeScores () {
		foreach (Transform child in transform) {
			if (child.name == "High Score Name" || child.name == "High Score Score") {
				Destroy (child);
			}
		}
	}

	static void addHighScore(string name, int score) {
		highScoreList.Add (new TextTuple (name, score));
		highScoreList.Sort (TextTupleComparator);
		while (highScoreList.Count > 5) {
			highScoreList.RemoveAt(highScoreList.Count-1);
		}
	}

	void renderScores() {
		GameObject root = this.gameObject;
		int index = 0;
		foreach (TextTuple t in HighscoreController.highScoreList) {
			GameObject hsn = Instantiate (highScoreName) as GameObject;
			GameObject hss = Instantiate (highScoreScore) as GameObject;
			hsn.transform.SetParent (root.transform);
			hss.transform.SetParent (root.transform);

			float posx = -50;
			float posy = 120 - index*50;
			float posz = 0;
			hsn.transform.localPosition = new Vector3(posx, posy, posz);
			hsn.GetComponent<Text>().text = t.name;
			
			float posa = 150;
			float posb = 120 - index*50;
			float posc = 0;
			hss.transform.localPosition = new Vector3(posa, posb, posc);
			hss.GetComponent<Text>().text = ""+t.score;

			index += 1;
		}
	}
}
