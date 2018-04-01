using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
	// Store score in ScoreKeeper class (works even if there is no object).
	public static int score = 0; 

	private Text myText;

	void Start () {
		myText = GetComponent<Text>();	
		Reset ();
		Score (0);  

	}

	public void Score (int points) {
		Debug.Log ("Scored points");
		score += points;
		myText.text = score.ToString ();
	}

	public static void Reset () {
		score = 0;
	}
}
