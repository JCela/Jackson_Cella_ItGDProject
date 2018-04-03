using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera3Script : MonoBehaviour {

	float cameraDistance = 3;
	public Camera MainCamera3;

	public Vector2 myDefaultRatio = new Vector2 (16, 9);
	private float myOrthographicSize;
	private Vector2 myLastScreenSize = Vector2.zero;

	// Use this for initialization
	void Start () {
		MainCamera3.enabled = true;
		MainCamera3.orthographic = true;
		myOrthographicSize = this.MainCamera3.orthographicSize;
		MainCamera3.rect = new Rect(0,0,1,1);
		//Camera.main.aspect = 1/2;
	}

	// Update is called once per frame
	void Update () {

		if (Screen.width != myLastScreenSize.x || Screen.height != myLastScreenSize.y) {

			//if ((float)Screen.height / (float)Screen.width > myDefaultRatio.y / myDefaultRatio.x) {
			MainCamera3.orthographicSize = myOrthographicSize * myDefaultRatio.x / myDefaultRatio.y / (float)Screen.width * (float)Screen.height;
			// else {
			//	MainCamera1.orthographicSize = myOrthographicSize;
			//}
			myLastScreenSize = new Vector2 (Screen.width, Screen.height);

		}
	}
}