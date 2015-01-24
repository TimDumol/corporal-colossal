using UnityEngine;
using System.Collections;

public class SheepMath : MonoBehaviour {
	public static float SqrMagnitude2D(Vector3 vec) {
		vec.y = 0;
		return vec.sqrMagnitude;
	}

	public static GameObject FindClosestUnsafeSheep(GameObject finder) {
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Sheep");
		float distance = float.MaxValue;
		GameObject closest = null;
		
		foreach(GameObject obj in objs) {
			if (obj.GetComponent<SheepController>().safe)
				continue;
			Vector3 v = finder.transform.position - obj.transform.position;
			float sqr = SheepMath.SqrMagnitude2D(v);
			if (sqr < distance) {
				closest = obj;
				distance = sqr;
			}
		}
		
		return closest;
	}

	
	public static GameObject FindClosestGameObjectWithTag(GameObject finder, string tag)
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag (tag);
		float distance = float.MaxValue;
		GameObject closest = null;
		
		foreach(GameObject obj in objs) {
			Vector3 v = finder.transform.position - obj.transform.position;
			float sqr = SheepMath.SqrMagnitude2D(v);
			if (sqr < distance) {
				closest = obj;
				distance = sqr;
			}
		}
		
		return closest;
	}
	
	public static Vector3 GetLocalScale (GameObject obj, float dx) {
		if (Mathf.Abs (dx) > 0) {
			float x = -Mathf.Sign (dx) * Mathf.Abs(obj.transform.localScale.x);
			float y = obj.transform.localScale.y;
			float z = obj.transform.localScale.z;
			return new Vector3(x, y, z);
		} else {
			return obj.transform.localScale;
		}
	}
}
