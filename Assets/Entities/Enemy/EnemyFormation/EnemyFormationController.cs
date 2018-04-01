using UnityEngine;
using System.Collections;

public class EnemyFormationController : MonoBehaviour {
	public GameObject enemyPrefab;
	public float zRotation = 180f; // Prefab rotation.
	public float width = 10f;
	public float height = 5f;
	public float speedX = 2f;
	public float padding = 0.5f;
	public float spawnDelay = 0.5f;

	private bool movingRight = true;
	private float xMin;
	private float xMax;

	//private int enemiesAlive = 0;

	// Use this for initialization
	void Start () {
		SpawnUntilFull ();

		// Store left and right boundaries based on main camera in xMin/xMax.
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceToCamera));
		xMax = rightBoundary.x;
		xMin = leftBoundary.x;
	}

	public void OnDrawGizmos () { Gizmos.DrawWireCube (transform.position, new Vector3(width, height));	}

	// Update is called once per frame
	void Update () {
		MoveFormation ();
		CountEnemiesAlive ();
		if (CheckAllMembersDead ()) {
			Debug.Log ("Empty Formation.");
			Debug.Log ("Respawning Formation.");
			SpawnUntilFull ();
		}

	}

	Transform NextFreePosition () {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}
		return null;
	}

	void CountEnemiesAlive () {
		int enemiesAlive = 0;
		foreach (Transform childPositionGameObject in transform) {
			enemiesAlive += childPositionGameObject.childCount;
		}
		Debug.Log ("Enemies alive: " + enemiesAlive);
		enemiesAlive = 0;


	}

	bool CheckAllMembersDead () {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}

	void SpawnEnemies () {
		// Spawn enemies at all position prefabs inside Enemy Formation.
		foreach (Transform child in transform) {
			// Specify instantiation of enemy as GameObject. Instantiate alone would return Object.
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.Euler(0, 0, zRotation)) as GameObject; 
			// Set the parent of the instantiated enemy to be the transform of the GameObject this script is attached to.
			enemy.transform.parent = child;
		}
	}

	void SpawnUntilFull () {
		Transform freePosition = NextFreePosition ();
		if (freePosition) {
			// Specify instantiation of enemy as GameObject. Instantiate alone would return Object.
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.Euler (0, 0, zRotation)) as GameObject; 
			// Set the parent of the instantiated enemy to be the transform of the GameObject this script is attached to.
			enemy.transform.parent = freePosition;
		}
		if (NextFreePosition ()) {
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}

	void MoveFormation () {
		// Set direction of movement.
		if (movingRight) {
			transform.position += Vector3.right * speedX * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speedX * Time.deltaTime;
		}
		// When edges of formation reach xMin/xMax (left and right boundaries), 
		// set new direction of movement explicitly.
		float rightEdgeOfFormation = transform.position.x + (0.5f * width);
		float leftEdgeOfFormation = transform.position.x - (0.5f * width);
		// If an edge exceeds x boundaries for two frames, direction gets inverted twice.
		if ( leftEdgeOfFormation < xMin ) {
			movingRight = true; 
		} else if (rightEdgeOfFormation > xMax) {
			movingRight = false;
		}
	}
}
