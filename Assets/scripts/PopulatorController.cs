using UnityEngine;
using System.Collections;

public class PopulatorController : MonoBehaviour
{
	public GameObject sheep;
	public GameObject enemy;
	public GameObject player;
	private Vector3 sheepSize;
	private Vector3 enemySize;

	void Awake ()
	{
		StateController.OnLevelStart += OnLevelStart;
		GameObject tmp = Instantiate (sheep, new Vector3 (-100, -100, -100), Quaternion.identity) as GameObject;
		sheepSize = tmp.collider.bounds.size;
		Destroy (tmp);
		tmp = Instantiate (enemy, new Vector3 (-100, -100, -100), Quaternion.identity) as GameObject;
		enemySize = tmp.collider.bounds.size;
		Destroy (tmp);
		Debug.Log (string.Format ("my sheep size is: {0}; {1}", sheepSize, enemySize));
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
				Instantiate (sheep, new Vector3 (x, 5f, z), Quaternion.Euler (90, 0, 0));
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
				GameObject e = (GameObject)Instantiate (enemy, new Vector3 (x, 5f, z), Quaternion.Euler (90, 0, 0));
				e.GetComponent<NavMeshAgent>().speed *= 1 + 0.20f*level;
                spawned = true;
            }
            
        } while (!spawned);
	}

	void SpawnPlayer() {
		Instantiate (player, new Vector3 (10, 0.05f, 0), Quaternion.Euler (90, 0, 0));
	}

	void OnLevelStart (int level)
	{
		SpawnPlayer ();
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
