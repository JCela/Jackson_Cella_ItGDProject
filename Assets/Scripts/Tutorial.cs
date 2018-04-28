using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.S)) {
			Mainmenu ();
		}
		if (Input.GetKey (KeyCode.K)) {
			Mainmenu ();
		}
	}

	void Mainmenu () {
		SceneManager.LoadScene ("MainMenuScreen");
	}
}
