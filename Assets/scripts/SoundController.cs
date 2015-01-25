using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {
	public AudioSource walkingSound;
	// Use this for initialization
	void Start () {
		walkingSound.enabled = true;
		//walkingSound.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
