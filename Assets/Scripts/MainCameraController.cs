using UnityEngine;
using System.Collections;

public class MainCameraController : MonoBehaviour {

	void OnDrawGizmos () {
		float height = 2f * camera.orthographicSize; // Works only with orthographic cameras.
		float width = height * camera.aspect;
		Gizmos.DrawWireCube (transform.position, new Vector3(width, height));
	}
}
