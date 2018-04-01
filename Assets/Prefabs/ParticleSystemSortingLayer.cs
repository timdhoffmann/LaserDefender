using UnityEngine;
using System.Collections;

public class ParticleSystemSortingLayer : MonoBehaviour {

	public int sortingLayerID, sortingLayerOrder;

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerID = sortingLayerID;
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = sortingLayerOrder;
	}
}
