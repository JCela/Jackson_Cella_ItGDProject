using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2Script : MonoBehaviour {

	public Transform playerTransform;
	float cameraDistance = 3;
	public Camera MainCamera2;

	// Use this for initialization
	void Start () {
		//Camera.main.aspect = 1/2;
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(
			transform.position.x, 
			playerTransform.position.y + cameraDistance , 
			transform.position.z
		);
		MainCamera2.enabled = true;
		MainCamera2.orthographic = true;
		MainCamera2.orthographicSize = 5.2f;
		MainCamera2.rect = new Rect(0.5f, 0, 1, 2);
	}
}
