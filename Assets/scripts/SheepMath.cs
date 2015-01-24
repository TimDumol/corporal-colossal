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
			float dist = SheepMath.DistanceBetween2DGameObjects(finder, obj);
			if (dist < distance) {
				closest = obj;
				distance = dist;
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
			float dist = SheepMath.DistanceBetween2DGameObjects(finder, obj);
			if (dist < distance) {
				closest = obj;
				distance = dist;
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

	public static float DistanceBetween2DGameObjects (GameObject aObj, GameObject bObj) {
		Bounds a = aObj.collider.bounds;
		Bounds b = bObj.collider.bounds;

		if (a.Intersects (b)) {
			return 0.0f;
		}

		Bounds left = a.min.x < b.min.x ? a : b;
		Bounds right = a.min.x < b.min.x ? b : a;
		float dx = 0;
		if (left.max.x < right.min.x) {
			dx = right.min.x - left.max.x;
		} else {
			dx = 0;
		}

		Bounds lower = a.min.z < b.min.z ? a : b;
		Bounds upper = a.min.z < b.min.z ? b : a;
		float dz = 0;
		if (lower.max.z < upper.min.z) {
			dz = upper.min.z - lower.max.z;
		} else {
			dz = 0;
		}

		return Mathf.Sqrt (dx * dx + dz * dz);
	}
}
