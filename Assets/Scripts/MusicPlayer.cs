using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	public AudioClip startClip, gameClip, endClip;

	private AudioSource music;

	static MusicPlayer instanciated = null;

	void Awake () {
		
		// check if musicPlayer has been loaded already. 
		// If so, destroy object before all Start methods get called, 
		// but only do so if it's not the current instance.
		if (instanciated != null && instanciated != this) {
			Destroy (gameObject);
		}
		else {
			instanciated = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource> ();
			// Start music for level 0 manually, because level 0 
			// never sends a "OnLevelWasLoaded" message.
			music.clip = startClip;
			music.time = 4.5f;
			music.loop = true;
			music.Play();
		}
	}

	void OnLevelWasLoaded (int Level) {
		Debug.Log ("MusicPlayer: loaded level " + Level);
		music.Stop ();
		if (Level == 0) {
			music.clip = startClip;
		}

		if (Level == 1) {
			music.clip = gameClip;
		}

		if (Level == 2) {
			music.clip = endClip;
		}
		music.loop = true;
		music.Play ();
	}
}

// issue: Another music player instance is shortly awake and lives until Start is executed