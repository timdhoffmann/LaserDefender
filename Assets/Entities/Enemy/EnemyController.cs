using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public GameObject laserPrefab;
	public float projectileSpeed;
	public float fireRatePerSecond = 0.5f;
	public float health = 150f;
	public int scoreValue = 150;
	public AudioClip shotSound, lethalSound;

	private ScoreKeeper scoreKeeper;

	// Initialization
	void Start () {
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

	void Update () {
		// Fire at random rate per second TUTORIAL.
		float probability = fireRatePerSecond * Time.deltaTime;
		if (Random.value < probability) {
			Fire ();
		}
	}

	void OnTriggerEnter2D (Collider2D collider) {
		// Check if incoming gameObject is a PlayerShot (has a PlayerShot component).
		PlayerShot shot = collider.gameObject.GetComponent<PlayerShot> ();

		if (shot) { 
			Debug.Log (collider.gameObject + " detected."); 
			// Apply damage.
			health -= shot.GetDamage();
			Debug.Log ("Health remaining: " + health);
			// Message shot of hit.
			shot.Hit ();
			// Check if damage is leathal.
			if (health <= 0) {
				shot.LethalHit ();
				AudioSource.PlayClipAtPoint (lethalSound, transform.position);
				Destroy (gameObject);
				scoreKeeper.Score (scoreValue);
			}

		}
	}

	void Fire () {
		// Instantiate LaserPrefab.
		GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.AngleAxis (180f, Vector3.forward)) as GameObject;
		// Add speed to projectile.
		laser.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, - projectileSpeed);
		AudioSource.PlayClipAtPoint (shotSound, transform.position);
	}
}
