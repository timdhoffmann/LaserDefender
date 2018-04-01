using UnityEngine;
using System.Collections;

public class ParticleSystemSortingLayer : MonoBehaviour {

	public int sortingLayerID, sortingLayerOrder;

	// Use this for initialization
	void Start () {
		particleSystem.renderer.sortingLayerID = sortingLayerID;
		particleSystem.renderer.sortingOrder = sortingLayerOrder;
	}
}
