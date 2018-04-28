using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour {
	public float x = 0.1f;
	public float y = 0.05f;
	public float t;
	public GameObject platformS;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		t += 0.1f;

		if (t > 0 && t < 10) {
			platformS.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (x, y), ForceMode2D.Impulse);
		}
		if (t > 10 && t <20) {
			platformS.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-x, -y), ForceMode2D.Impulse);
		}
		if (t > 30) {
			t = 0;
		}
			


	}
}
