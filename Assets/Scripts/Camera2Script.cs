using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2Script : MonoBehaviour {

	public Transform playerTransform;
	float cameraDistance = 3;
	public Camera MainCamera2;

	public Vector2 myDefaultRatio = new Vector2 (16, 9);
	private float myOrthographicSize;
	private Vector2 myLastScreenSize = Vector2.zero;

	// Use this for initialization
	void Start () {
		MainCamera2.enabled = true;
		MainCamera2.orthographic = true;
		myOrthographicSize = this.MainCamera2.orthographicSize;
		MainCamera2.rect = new Rect(0.5f, 0, 1, 2);
		//Camera.main.aspect = 1/2;
	}

	// Update is called once per frame
	void Update () {
		
		if (Screen.width != myLastScreenSize.x || Screen.height != myLastScreenSize.y) {

			//if ((float)Screen.height / (float)Screen.width > myDefaultRatio.y / myDefaultRatio.x) {
			MainCamera2.orthographicSize = myOrthographicSize * myDefaultRatio.x / myDefaultRatio.y / (float)Screen.width * (float)Screen.height;
			// else {
			//	MainCamera1.orthographicSize = myOrthographicSize;
			//}
			myLastScreenSize = new Vector2 (Screen.width, Screen.height);

		}

		transform.position = new Vector3(
			transform.position.x, 
			playerTransform.position.y + cameraDistance , 
			transform.position.z
		);

	}
}
