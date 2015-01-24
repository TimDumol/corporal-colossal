using UnityEngine;
using System.Collections;

public class SheepMath : MonoBehaviour {
	public static float SqrMagnitude2D(Vector3 vec) {
		vec.y = 0;
		return vec.sqrMagnitude;
	}
}
