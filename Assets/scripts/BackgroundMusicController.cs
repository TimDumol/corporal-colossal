using UnityEngine;
using System.Collections;

public class BackgroundMusicController : MonoBehaviour {
	public AudioClip bleep1;
	public AudioClip bleep2;
	public AudioClip bleep3;
	private AudioClip[] bleeps;
	private bool bleeping;

	void Awake () {
		bleeps = new [] {bleep1, bleep2, bleep3};
		StateController.OnLevelStart += StartBleeping;
		StateController.OnLevelEnd += StopBleeping;
	}

	void StartBleeping(int level) {
		bleeping = true;
		StartCoroutine (BleepOccasionally ());
	}

	void StopBleeping(int level) {
		StopCoroutine ("BleepOccasionally");
		bleeping = false;
	}

	IEnumerator BleepOccasionally() {
		while (bleeping) {
			int index = Random.Range (0, 2);
			AudioSource.PlayClipAtPoint(bleeps[index], Vector3.zero, 0.2f);
			float waitTime = 1 + Random.value * 4;
			yield return new WaitForSeconds(waitTime);
		}
	}
}
