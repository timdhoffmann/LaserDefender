using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndScore : MonoBehaviour {

	private Text text;
	private ScoreKeeper scoreKeeper;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		text.text = ScoreKeeper.score.ToString ();
		ScoreKeeper.Reset ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
