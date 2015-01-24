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

	void SpawnSheep (int level)
	{
		bool spawned = false;
		do {
			float z = Random.Range (GameProperties.bottom, GameProperties.top);
			float x = Random.Range (GameProperties.left, GameProperties.right);
			Debug.Log (string.Format ("Generated: {0}, {1}; sheep size is: {2} ", x, z, sheepSize));
			if (!Physics.CheckCapsule (new Vector3(x - sheepSize.x/2f, z/2f + 0.5f, z), new Vector3(x + sheepSize.x/2f, z/2f + 0.5f, z), z/2f)) {
				Debug.Log (string.Format ("Pass: {0}, {1}; sheep size is: {2} ", x, z, sheepSize));
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
			Debug.Log (string.Format ("Generated: {0}, {1}; enemy size is: {2} ", x, z, enemySize));
			if (!Physics.CheckCapsule (new Vector3(x - enemySize.x/2f, z/2f + 0.5f, z), new Vector3(x + enemySize.x/2f, z/2f + 0.5f, z), z/2f)) {
				Debug.Log (string.Format ("Pass: {0}, {1}; enemy size is: {2} ", x, z, enemySize));
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
