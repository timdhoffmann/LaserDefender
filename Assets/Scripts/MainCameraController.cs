using UnityEngine;
using System.Collections;

public class MainCameraController : MonoBehaviour {

	void OnDrawGizmos () {
		float height = 2f * GetComponent<Camera>().orthographicSize; // Works only with orthographic cameras.
		float width = height * GetComponent<Camera>().aspect;
		Gizmos.DrawWireCube (transform.position, new Vector3(width, height));
	}
}
