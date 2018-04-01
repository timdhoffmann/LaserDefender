using UnityEngine;
using System.Collections;

public class PlayerShot : MonoBehaviour {

	public float damage = 100f;
	public GameObject laserExplosionPrefab;
	public GameObject lethalExplosionPrefab;

	public float GetDamage () {
		return damage;
	}

	public void Hit () {
		Destroy (gameObject);
		Instantiate (laserExplosionPrefab, transform.position, Quaternion.identity);
	}

	public void LethalHit () {
		Destroy (gameObject);
		Instantiate (lethalExplosionPrefab, transform.position, Quaternion.identity);
	}
}
