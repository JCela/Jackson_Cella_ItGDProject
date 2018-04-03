using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

	Collider2D platformCollider;

	//Player1Script is

	// Use this for initialization
	void Start () {

		platformCollider = this.GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		//if (grounded == true)
	}
}
