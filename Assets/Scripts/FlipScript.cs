using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlipScript : MonoBehaviour {

	// Use this for initialization
	GameObject MainCamera2;
	GameObject MainCamera1;

	private float pickupTimer1 = 2;

	private float pickupTimer2 = 2;

	void Start () {
		MainCamera2 = GameObject.Find ("Main Camera 2");
		MainCamera1 = GameObject.Find ("Main Camera 1");
		Debug.Log (MainCamera2);
	}

	void Update () {
		Debug.Log (pickupTimer1+" "+pickupTimer2);

		pickupTimer1 = pickupTimer1 + 1 * Time.deltaTime;
		pickupTimer2 = pickupTimer2 + 1 * Time.deltaTime;
		if (pickupTimer1 > 0) {
			MainCamera2.transform.rotation = Quaternion.identity;
		}
		if (pickupTimer2 > 0) {
			MainCamera1.transform.rotation = Quaternion.identity;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.CompareTag ("Player 1")) {
			Pickup1 ();
		}
		if (other.CompareTag ("Player 2")) {
			Pickup2 ();
		}
	}

	void Pickup1 () {
		Debug.Log ("Power up picked up");
		pickupTimer1 = -5;
		MainCamera2.transform.Rotate (0, 0, 180);
		Destroy (gameObject);
	}

	void Pickup2 () {
		Debug.Log ("Power 2 picked up");
		pickupTimer2 = -5;
		MainCamera1.transform.Rotate (0, 0, 180);
		Destroy (gameObject);
	}
}
