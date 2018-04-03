using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1Script : MonoBehaviour {

	public Transform playerTransform;
	public static float cameraDistance = 3;
	public Camera MainCamera1;

	public Vector2 myDefaultRatio = new Vector2 (16, 9);
	public static float myOrthographicSize;
	private Vector2 myLastScreenSize = Vector2.zero;
	// Use this for initialization
	void Start () {
		MainCamera1.enabled = true;
		MainCamera1.orthographic = true;
		MainCamera1.rect = new Rect(0, 0, 0.5f, 1);
		myOrthographicSize = this.MainCamera1.orthographicSize;
		//myLastScreenSize = new Vector2 (Screen.width, Screen.height);
		//Camera.main.aspect = 1/2;
	}
	
	// Update is called once per frame
	void Update () {



		if (Screen.width != myLastScreenSize.x || Screen.height != myLastScreenSize.y) {

			//if ((float)Screen.height / (float)Screen.width > myDefaultRatio.y / myDefaultRatio.x) {
				MainCamera1.orthographicSize = myOrthographicSize * myDefaultRatio.x / myDefaultRatio.y / (float)Screen.width * (float)Screen.height;
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
