using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1Script : MonoBehaviour {

	public Transform playerTransform;
	float cameraDistance = 3;
	public Camera MainCamera1;

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
		MainCamera1.enabled = true;
		MainCamera1.orthographic = true;
		MainCamera1.orthographicSize = 5.2f;
		MainCamera1.rect = new Rect(0, 0, 0.5f, 1);

	}
}
