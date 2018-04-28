using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerScript : MonoBehaviour {
	bool ready1 = false;
	bool ready2 = false;
	public GameObject check1;
	public GameObject check2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W)) {
			check1.SetActive (true);
			ready1 = true;
		}
		if (Input.GetKey (KeyCode.I)) {
			check2.SetActive (true);
			ready2 = true;
		}
		if (ready1 == true && ready2 == true) {
			SceneManager.LoadScene ("MainMenuScreen");
		}
	}
}
