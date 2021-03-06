﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player1Script : MonoBehaviour {

	SpriteRenderer player;
	Rigidbody2D playerRigid;
    Collider2D playerCollider;
	public Animator Player1Animator;
	public GameObject HUD;
	GameObject MainCamera2;
	public GameObject ScreenSplat2;
	public LayerMask whatToHit;
	public Sprite victorypng;
	public GameObject BulletSpritePrefab;
	public AudioSource PowerUpSound;
	public AudioSource SplatSound;

	float jumpForce = 14;
	float speed = 0.1f;
	float shootTimer = 20;
	float freezeTimer = 5;
	float dash = 0.25f;
	float dashspeed = .5f;
	float dashCounter = -5;
	float canDashTimer = 0;
	private float pickupTimer1 = 2;
	private float speedTimer1 = 2;
	private float splatTimer1 = 2;

	bool isLeft = true;
	bool hit = false;
	bool canShoot = false;
	bool grounded = true;
	bool freeze = false;
	bool p1win = false;

	Transform firePoint;
	Transform leftEdge;
	Transform rightEdge;
	Transform middleEdge;
	Transform groundDetector;
	Transform firedirection1;
	Transform firedirection2;
	Transform firedirection3;
	Transform vicDetector;





	// Use this for initialization

	void Start () {

		MainCamera2 = GameObject.Find ("Main Camera 2");//Camera
		ScreenSplat2 = GameObject.Find ("ScreenSplat2");
		player = this.GetComponent<SpriteRenderer> ();
		playerRigid = this.GetComponent<Rigidbody2D> ();
		playerCollider = this.GetComponent<Collider2D> ();
		groundDetector = transform.Find("GroundDetector");
		vicDetector = transform.Find ("FinishDetector");
		firePoint = transform.Find ("firePoint");

		firedirection1 = transform.Find ("firedirection1");
		firedirection2 = transform.Find ("firedirection2");
		firedirection3 = transform.Find ("firedirection3");

		leftEdge = GameObject.Find ("LeftEdge").transform;
		rightEdge = GameObject.Find ("RightEdge").transform;
		middleEdge = GameObject.Find ("MiddleEdge").transform;
	}
	

	void Update () {
		//*********Movement**********//
		dashCounter += 1*Time.deltaTime; //how often you can dash

		canDashTimer -= 1 * Time.deltaTime; //timer for how long the dash lasts
		pickupTimer1 = pickupTimer1 + 1 * Time.deltaTime; //timer for how long the flip lasts
		speedTimer1 = speedTimer1 + 1 * Time.deltaTime; //timer for how long speed up lasts
		splatTimer1 = splatTimer1 + 1 * Time.deltaTime;//timer for how long splat lasts

		if (splatTimer1 < 0) {
			ScreenSplat2.SetActive (true);
		} else {
			ScreenSplat2.SetActive (false);
		}

		if (pickupTimer1 > 0) {
			MainCamera2.transform.rotation = Quaternion.identity;
		}

		grounded = Physics2D.Linecast(transform.position, groundDetector.position, 1 << LayerMask.NameToLayer("Ground")); //uses linecast to determine if the player is grounded
		if (p1win == false) {
			p1win = Physics2D.Linecast (transform.position, vicDetector.position, 1 << LayerMask.NameToLayer ("Finish"));
		}

		if (speedTimer1 < 0) {
			if (Input.GetKey (KeyCode.A)) {
				this.GetComponent<Transform> ().Translate (new Vector3 (-0.2f, 0));
			}
			if (Input.GetKey (KeyCode.D)) {
				this.GetComponent<Transform> ().Translate (new Vector3 (0.2f, 0));
			}
			if (Input.GetKeyDown (KeyCode.W) && grounded) {
				playerRigid.velocity = new Vector2 (playerRigid.velocity.x, jumpForce); // Jump mechanic, checks to see if player is Grounded to jump
				grounded = false;
			}
		}

		//Movement left and right
		if (freeze == false && speedTimer1 > 0 && canDashTimer < 0) {
			if (Input.GetKey (KeyCode.A)) {
				this.GetComponent<Transform> ().Translate (new Vector3 (-speed, 0));
			}
			if (Input.GetKey (KeyCode.D)) {
				this.GetComponent<Transform> ().Translate (new Vector3 (speed, 0));
			} 
			//Jump
			if (Input.GetKeyDown (KeyCode.W) && grounded) {
				playerRigid.velocity = new Vector2 (playerRigid.velocity.x, jumpForce); // Jump mechanic, checks to see if player is Grounded to jump
				grounded = false;	
			}
		}

		//Animation stuff
		//******************
		bool isRunning = false;
		bool isJumping = false;
		bool isFrozen = false;
		if (Input.GetKey (KeyCode.D)) {
			isRunning = true;
			player.flipX = false;
		}
		if (Input.GetKey (KeyCode.A)) {
			isRunning = true;
			player.flipX = true;
		}
		if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.D)) {
			isRunning = false;
		}
		if (Input.GetKey (KeyCode.W)) {
			isJumping = true;
		}
		if (grounded) {
			isJumping = false;
		}
		if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.A)) {
			isRunning = false;
		}
		if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.D)) {
			isRunning = false;
		}
		Player1Animator.SetBool ("isRunning", isRunning);
		Player1Animator.SetBool ("isJumping", isJumping);
		//******************

		//Dashing
		if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.LeftShift) && dashCounter > 0) {
				playerRigid.velocity = Vector2.zero;
				playerRigid.gravityScale = 0;
				isLeft = true;
				//playerRigid.AddForce (Vector2.left*30, ForceMode2D.Impulse);
				dashCounter = -1;
				canDashTimer = .1f;
		}
		if (Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.LeftShift) && dashCounter > 0) {
			playerRigid.velocity = Vector2.zero;
			playerRigid.gravityScale = 0;
			isLeft = false;
			//playerRigid.AddForce (Vector2.right*30, ForceMode2D.Impulse);
			//this.GetComponent<Transform> ().Translate (new Vector3 (dashspeed, 0));
			dashCounter = -1;
			canDashTimer = .1f;
		}
		if (canDashTimer > 0) {
			if (isLeft == true) {
				this.GetComponent<Transform> ().Translate (new Vector3 (-dashspeed, 0));
			} else {
				this.GetComponent<Transform> ().Translate (new Vector3 (dashspeed, 0));
			}
		}
		if (canDashTimer < 0) {
			playerRigid.gravityScale = 4;
		}

	//Boundaries of the level
			if (transform.position.x < leftEdge.position.x) {
				transform.position = new Vector2 (leftEdge.position.x, transform.position.y); //Stops the player from moving past the "RightEdge" Object

			}
			if (transform.position.x > rightEdge.position.x) {
				transform.position = new Vector2 (rightEdge.position.x, transform.position.y); 

			}
			if (transform.position.x > middleEdge.position.x) {
				transform.position = new Vector2 (middleEdge.position.x, transform.position.y); 

			}

	

		//********Shooting*********//

		shootTimer -= 10 * Time.deltaTime;

		if (shootTimer <= 0) {
			canShoot = true;
		}

		if (canShoot == true) {
			if (Input.GetKey (KeyCode.R)) {
				Shoot1 ();
				canShoot = false;
				shootTimer = 20;
			}
			if (Input.GetKey (KeyCode.F)) {
				Shoot2 ();
				canShoot = false;
				shootTimer = 20;
			}
			if (Input.GetKey (KeyCode.C)) {
				Shoot3 ();
				canShoot = false;
				shootTimer = 20;
			}

		}

		//if hit you get frozen
		if (hit == true) {
			freezeTimer -= 1.75f * Time.deltaTime;
			if (freezeTimer > 0 && freezeTimer < 5) {
				freeze = true;
				isFrozen = true;
				isRunning = false;
				isJumping = false;
			}
			if (freezeTimer < 0) {
				freeze = false;
				freezeTimer = 5;
				hit = false;
				isFrozen = false;
			}

			Player1Animator.SetBool ("isFrozen", isFrozen);


		}

		//*******Winning*********//

		if (p1win == true) {
			HUD.SetActive (true);
			freeze = true;
			SceneManager.LoadScene ("Winner Player 1");
		}
	}


	void Shoot1 () {
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		Vector2 firedirectionPosition = new Vector2 (firedirection1.position.x, firedirection1.position.y);
		//RaycastHit2D hit = Physics2D.Raycast (firePointPosition, firedirectionPosition-firePointPosition, 1000, whatToHit);
		GameObject bulletS = Instantiate (BulletSpritePrefab, firePoint.position, firePoint.rotation );
		//bulletS.transform.up = Vector3.right;
		bulletS.GetComponent<BulletScript> ().myCaster = this.gameObject;
		bulletS.GetComponent<Rigidbody2D>().AddForce ((firedirectionPosition-firePointPosition).normalized*10, ForceMode2D.Impulse);
		//if (hit.collider != null) {
		//}
	}
	void Shoot2 () {
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		Vector2 firedirectionPosition2 = new Vector2 (firedirection2.position.x, firedirection2.position.y);
		//RaycastHit2D hit = Physics2D.Raycast (firePointPosition, firedirectionPosition2-firePointPosition, 1000, whatToHit);
		GameObject bulletS = Instantiate (BulletSpritePrefab, firePoint.position, firePoint.rotation);
		bulletS.transform.up = Vector3.right;
		bulletS.GetComponent<BulletScript> ().myCaster = this.gameObject;
		bulletS.GetComponent<Rigidbody2D>().AddForce ((firedirectionPosition2-firePointPosition).normalized*10, ForceMode2D.Impulse);
		//if (hit.collider != null) {
		//}
	}
	void Shoot3 () {
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		Vector2 firedirectionPosition3 = new Vector2 (firedirection3.position.x, firedirection3.position.y);
		GameObject bulletS = Instantiate (BulletSpritePrefab, firePoint.position, firePoint.rotation);
		bulletS.transform.up = Vector3.right;
		bulletS.GetComponent<BulletScript> ().myCaster = this.gameObject;
		bulletS.GetComponent<Rigidbody2D>().AddForce ((firedirectionPosition3-firePointPosition).normalized*10, ForceMode2D.Impulse);


	}
	void OnTriggerEnter2D(Collider2D otherObjectT) {
		CollisionDetection (otherObjectT.gameObject);
		if (otherObjectT.CompareTag ("PowerUp")) {
			Pickup1 ();
			Destroy (otherObjectT);
		}
		if (otherObjectT.CompareTag ("Speed")) {
			PickupSpeed1 ();
			Destroy (otherObjectT);
		}
		if (otherObjectT.CompareTag ("Splat")) {
			PickupSplat1 ();
			Destroy (otherObjectT);
		
		}
	}
	void OnCollisionEnter2D(Collision2D otherObjectC){
		CollisionDetection (otherObjectC.gameObject);
	}
	void CollisionDetection (GameObject otherObjectD) {
		if (otherObjectD.tag == "Bullet" && otherObjectD.GetComponent<BulletScript>().myCaster != this.gameObject) {
			Destroy (otherObjectD);
			hit = true;
		}
	}

	void Pickup1 () {
		pickupTimer1 = -5;
		MainCamera2.transform.Rotate (0, 0, 180);
		PowerUpSound.Play ();
	}
	void PickupSpeed1 () {
		speedTimer1 = -5;
		PowerUpSound.Play ();

	}
	void PickupSplat1 () {
		splatTimer1 = -5;
		Debug.Log ("PIcked up splat");
		SplatSound.Play ();
	
	}

}
