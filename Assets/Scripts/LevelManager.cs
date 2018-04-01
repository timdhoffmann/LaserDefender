using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel (string name) {
		Debug.Log ("Level load requested for: " + name);
		Application.LoadLevel (name);
	}
	
	public void LoadNextLevel () {
		// Load next level (by index as set in build settings)
		Application.LoadLevel (Application.loadedLevel + 1);
		Debug.Log ("Loading next level.");
	}
}
