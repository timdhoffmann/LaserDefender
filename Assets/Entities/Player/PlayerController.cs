using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speedX = 2.0f;
	public float padding = 0.5f;
	public GameObject laserPrefab;
	public float projectileSpeed;
	public float fireRate = 0.2f;
	public float health = 250f;
	public AudioClip shotSound, hitTakenSound, lethalHitSound;

	float xMin;
	float xMax;

	// Use this for initialization
	void Start () {
		// Set world borders based on camera/viewport.
		float distance = transform.position.z - Camera.main.transform.position.z; // Distance between object and camera.
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)); // This Vector3 uses values between 0 and 1 to represent viewport dimensions.
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		xMin = leftmost.x + padding;
		xMax = rightmost.x - padding;
	}
	
	// Update is called once per frame
	void Update () {
		// Control horizontal movement with buttons for horizontal movement.
		float translationX = Input.GetAxis("Horizontal") * speedX * Time.deltaTime; // multiply with Time.deltaTime for frame rate independance.
		transform.Translate(translationX, 0, 0);
		
		// Restrict x-position to gamespace
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
		// Fire at constant rate.
		if (Input.GetButtonDown("Fire1")) 	{ InvokeRepeating ("Fire", 0.000001f, fireRate); } // Set start time != 0 in order to avoid multi-invoke bug.
		if (Input.GetButtonUp ("Fire1")) 	{ CancelInvoke ("Fire"); }
	}

	void Fire () {
		// Instantiate LaserPrefab.
		Vector3 laserPosition = transform.position + new Vector3(-0.17f, 0.18f, 0.1f);
		GameObject laser = Instantiate(laserPrefab, laserPosition, Quaternion.identity) as GameObject;
		// Add speed to projectile.
		laser.rigidbody2D.velocity = new Vector3 (0f, projectileSpeed, 0f);
		AudioSource.PlayClipAtPoint (shotSound, transform.position);
	}

	void OnTriggerEnter2D (Collider2D collider) {
		// Check if incoming gameObject is a PlayerShot (has a PlayerShot component).
		EnemyShot shot = collider.gameObject.GetComponent<EnemyShot> ();
		if (shot) { 
			Debug.Log (collider.gameObject + " detected."); 
			// Apply damage.
			health -= shot.GetDamage();
			AudioSource.PlayClipAtPoint (hitTakenSound, transform.position);
			Debug.Log ("Player health remaining: " + health);
			// Message shot of hit.
			shot.Hit ();
			// Check if damage is leathal.
			if (health <= 0) {
				shot.LethalHit ();
				Death ();
			} 
			

		}
	}

	void Death () {
		AudioSource.PlayClipAtPoint (lethalHitSound, transform.position);
		Destroy (gameObject);
		LevelManager manager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		manager.LoadLevel ("Win");
	}
}
