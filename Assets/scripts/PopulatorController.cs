using UnityEngine;
using System.Collections;

public class PopulatorController : MonoBehaviour
{
	public GameObject sheep;
	public GameObject enemy;
	private Vector3 sheepSize;
	private Vector3 enemySize;

	void Awake ()
	{
		StateController.OnLevelStart += OnLevelStart;
		sheepSize = sheep.transform.localScale;
		enemySize = enemy.transform.localScale;
	}
	// Use this for initialization
	void Start ()
	{

		//Debug.Log (string.Format ("my size is : {0}; {1}", sheepSize, sheep.collider.bounds));
	}

	bool canPut(Vector3 size, float x, float z)
	{
		Vector3 penSize = GameObject.Find("Sheep Pen").transform.localScale;
		if (Mathf.Abs(x) < penSize.x && Mathf.Abs(z) < penSize.z)
			return false;
		Vector3 start = new Vector3(x, size.x / 2f + 0.5f, z - size.z / 2f);
		Vector3 end = new Vector3(x, size.x / 2f + 0.5f, z + size.z / 2f);
		float radius = size.x / 2f;
		return (!Physics.CheckCapsule(start, end ,radius));
	}
	
	void SpawnSheep (int level)
	{
		bool spawned = false;
		do {
			float z = Random.Range (GameProperties.bottom, GameProperties.top);
			float x = Random.Range (GameProperties.left, GameProperties.right);
			if (canPut(sheepSize, x, z)) {
				Instantiate (sheep, new Vector3 (x, 0.5f, z), Quaternion.identity);
				spawned = true;
			}
		} while (!spawned);
	}

	void SpawnEnemy (int level)
	{
		bool spawned = false;
		do {
			float z = Random.Range (GameProperties.bottom, GameProperties.top);
			float x = Random.Range (GameProperties.left, GameProperties.right);
			if (canPut(enemySize, x, z)) {
				GameObject e = (GameObject)Instantiate (enemy, new Vector3 (x, 0.5f, z), Quaternion.identity);
				e.GetComponent<NavMeshAgent>().speed *= 1 + 0.20f*level;
				spawned = true;
			}
        } while (!spawned);
	}

	void OnLevelStart (int level)
	{
		for (int i = 0; i < 2 + level; ++i) {
			SpawnSheep (level);
		}
		SpawnEnemy (level);
	}

	// Update is called once per frame
	void Update ()
	{

	}
}
