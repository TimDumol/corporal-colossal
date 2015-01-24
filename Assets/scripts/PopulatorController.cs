using UnityEngine;
using System.Collections;

public class PopulatorController : MonoBehaviour
{
	public float enemySpawnTimer;
	public GameObject sheep;
	public GameObject enemy;
	public GameObject player;
	private Vector3 sheepSize;
	private Vector3 enemySize;
	private Vector3 playerSize;

	void Awake ()
	{
		StateController.OnLevelStart += OnLevelStart;
		GameObject tmp = Instantiate (sheep, new Vector3 (-100, -100, -100), Quaternion.identity) as GameObject;
		sheepSize = tmp.collider.bounds.size;
		Destroy (tmp);
		tmp = Instantiate (enemy, new Vector3 (-1000, -1000, -1000), Quaternion.identity) as GameObject;
		enemySize = tmp.collider.bounds.size;
		Destroy (tmp);
		tmp = Instantiate (player, new Vector3 (-10000, -10000, -10000), Quaternion.identity) as GameObject;
		playerSize = tmp.collider.bounds.size;
		Destroy (tmp);
		Debug.Log (string.Format ("my sheep size is: {0}; {1}; {2}", sheepSize, enemySize, playerSize));
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
				Instantiate (sheep, new Vector3 (x, sheepSize.y/2, z), Quaternion.identity);
				spawned = true;
			}
		} while (!spawned);
	}

	IEnumerator SpawnEnemy (int level)
	{
		yield return new WaitForSeconds(enemySpawnTimer);
		bool spawned = false;
		do {
			float z = GameProperties.bottom + 10.0f;
			float x = (GameProperties.left+GameProperties.right)/2.0f;
			if (canPut(enemySize, x, z)) {
				GameObject e = (GameObject)Instantiate (enemy, new Vector3 (x, enemySize.y/2f, z), Quaternion.identity);
				e.GetComponent<NavMeshAgent>().speed *= 1 + 0.10f*level;
				spawned = true;
			}
        } while (!spawned);
	}

	void SpawnPlayer() {
		Instantiate (player, new Vector3 (30, playerSize.y/2, 0), Quaternion.identity);
	}

	void OnLevelStart (int level)
	{
		SpawnPlayer ();
		for (int i = 0; i < 2 + level; ++i) {
			SpawnSheep (level);
		}
		StartCoroutine(SpawnEnemy (level));

	}

	// Update is called once per frame
	void Update ()
	{

	}
}
