using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Script : MonoBehaviour {

	Collider2D playerCollider;
	SpriteRenderer player;
	Rigidbody2D playerRigid;
	public LayerMask whatToHitP;
	public GameObject BulletSpritePrefab;
	public GameObject HUD;
	GameObject MainCamera1;
	public GameObject ScreenSplat1;
	public Animator Player2Animator;

	float speed = 0.1f;
	float jumpForce = 14;
	float shootTimer = 20;
	float freezeTimer = 5;
	float dashCounter = 5;
	float canDashTimer2 = 0;
	float dashSpeed2 = 0.5f;

	bool hit = false;
	bool freeze = false;
	bool canShoot = false;
	bool groundedtwo = true;
	bool p2win = false;
	bool isLeft = false;

	Transform groundDetector;

	Transform firePointP;
	Transform leftEdge;
	Transform rightEdge;
	Transform middleEdge;
	Transform firedirection1P;
	Transform firedirection2P;
	Transform firedirection3P;
	Transform vicDetector2;

	private float pickupTimer2 = 2;
	private float speedTimer2 = 2;
	private float splatTimer2 = 2;




	// Use this for initialization
	void Start () {
		MainCamera1 = GameObject.Find ("Main Camera 1");
		ScreenSplat1 = GameObject.Find ("ScreenSplat1");

		player = this.GetComponent<SpriteRenderer> ();

		playerRigid = this.GetComponent<Rigidbody2D> ();

		playerCollider = this.GetComponent<Collider2D> ();

		groundDetector = transform.Find("GroundDetector2");
		vicDetector2 = transform.Find ("FinishDetector2");

		firePointP = transform.Find ("firePointP");
		firedirection1P = transform.Find ("firedirection1P");
		firedirection2P = transform.Find ("firedirection2P");
		firedirection3P = transform.Find ("firedirection3P");


		leftEdge = GameObject.Find ("LeftEdge").transform;
		rightEdge = GameObject.Find ("RightEdge").transform;
		middleEdge = GameObject.Find ("MiddleEdge").transform;
	}

	// Update is called once per frame
	void Update () {
		
		pickupTimer2 = pickupTimer2 + 1 * Time.deltaTime;
		speedTimer2 = speedTimer2 + 1 * Time.deltaTime;
		splatTimer2 = splatTimer2 + 1 * Time.deltaTime;

		if (pickupTimer2 > 0) {
			MainCamera1.transform.rotation = Quaternion.identity;
		}

		if (splatTimer2 < 0) {
			ScreenSplat1.SetActive (true);
		} else {
			ScreenSplat1.SetActive (false);
		}

		groundedtwo = Physics2D.Linecast(transform.position, groundDetector.position, 1 << LayerMask.NameToLayer("Ground")); //uses linecast to determine if the player is grounded
		if (p2win == false) {
			p2win = Physics2D.Linecast (transform.position, vicDetector2.position, 1 << LayerMask.NameToLayer ("Finish"));
		}


		dashCounter += 1 * Time.deltaTime;
		canDashTimer2 -= 1 * Time.deltaTime;
		if (speedTimer2 < 0) {
			if (Input.GetKey (KeyCode.J)) {
				this.GetComponent<Transform> ().Translate (new Vector3 (-0.2f, 0));
			}
			if (Input.GetKey (KeyCode.L)) {
				this.GetComponent<Transform> ().Translate (new Vector3 (0.2f, 0));
			}
			if (Input.GetKeyDown (KeyCode.I) && groundedtwo) {
				playerRigid.velocity = new Vector2 (playerRigid.velocity.x, jumpForce); // Jump mechanic, checks to see if player is Grounded to jump
				groundedtwo = false;
			}
		}

		bool isRunning2 = false;
		bool isJumping2 = false;
		bool isFrozen2 = false;
		if (Input.GetKey (KeyCode.L)) {
			isRunning2 = true;
			player.flipX = false;
		}
		if (Input.GetKey (KeyCode.J)) {
			isRunning2 = true;
			player.flipX = true;
		}
		if (Input.GetKey (KeyCode.J) && Input.GetKey (KeyCode.L)) {
			isRunning2 = false;
		}
		if (Input.GetKey (KeyCode.I)) {
			isJumping2 = true;
		}
		if (groundedtwo) {
			isJumping2 = false;
		}
		if (Input.GetKey (KeyCode.I) && Input.GetKey (KeyCode.J)) {
			isRunning2 = false;
		}
		if (Input.GetKey (KeyCode.I) && Input.GetKey (KeyCode.J)) {
			isRunning2 = false;
		}

		Player2Animator.SetBool ("isRunning2", isRunning2);
		Player2Animator.SetBool ("isJumping2", isJumping2);

		if (freeze == false && speedTimer2 > 0 && canDashTimer2 <0) {
			if (Input.GetKey (KeyCode.J)) {
				this.GetComponent<Transform> ().Translate (new Vector3 (-speed, 0));
				//if (Input.GetKey (KeyCode.Semicolon) && dashCounter < 0) {
					//playerRigid.velocity = new Vector2 (-12, playerRigid.velocity.y);
				//	playerRigid.AddForce (Vector2.left*9, ForceMode2D.Impulse);
				//	dashCounter = 5;
				//}
			}
			if (Input.GetKey (KeyCode.L)) {
				this.GetComponent<Transform> ().Translate (new Vector3 (speed, 0));
				//if (Input.GetKey (KeyCode.Semicolon) && dashCounter < 0) {
					//playerRigid.velocity = new Vector2 (-12, playerRigid.velocity.y);
				//	playerRigid.AddForce (Vector2.right*9, ForceMode2D.Impulse);
				//	dashCounter = 5;
				//}
			}

			if (Input.GetKeyDown (KeyCode.I) && groundedtwo) {
				//playerRigid.AddForce (Vector2.up*jumpForce, ForceMode2D.Impulse);
				playerRigid.velocity = new Vector2 (playerRigid.velocity.x, jumpForce); // Jump mechanic, checks to see if player is Grounded to jump
				groundedtwo = false;
			}
		}

		if (Input.GetKey (KeyCode.J) && Input.GetKey (KeyCode.RightShift) && dashCounter > 0) {
			playerRigid.velocity = Vector2.zero;
			playerRigid.gravityScale = 0;
			isLeft = true;
			//playerRigid.AddForce (Vector2.left*30, ForceMode2D.Impulse);
			dashCounter = -1;
			canDashTimer2 = .1f;
		}
		if (Input.GetKey (KeyCode.L) && Input.GetKey (KeyCode.RightShift) && dashCounter > 0) {
			playerRigid.velocity = Vector2.zero;
			playerRigid.gravityScale = 0;
			isLeft = false;
			//playerRigid.AddForce (Vector2.right*30, ForceMode2D.Impulse);
			//this.GetComponent<Transform> ().Translate (new Vector3 (dashspeed, 0));
			dashCounter = -1;
			canDashTimer2 = .1f;
		}
		if (canDashTimer2 > 0) {
			if (isLeft == true) {
				this.GetComponent<Transform> ().Translate (new Vector3 (-dashSpeed2, 0));
			} else {
				this.GetComponent<Transform> ().Translate (new Vector3 (dashSpeed2, 0));
			}
		}
		if (canDashTimer2 < 0) {
			playerRigid.gravityScale = 4;
		}
		//if (groundedtwo == false) {
		//	playerCollider.isTrigger = true;
		//} else {
		//	playerCollider.isTrigger = false;
		//}

		if (transform.position.x < leftEdge.position.x) {
			transform.position = new Vector2(leftEdge.position.x, transform.position.y); //Stops the player from moving past the "RightEdge" Object

		}
		if (transform.position.x > rightEdge.position.x) {
			transform.position = new Vector2(rightEdge.position.x, transform.position.y); 

		}
		if (transform.position.x < middleEdge.position.x) {
			transform.position = new Vector2(middleEdge.position.x+0.2f, transform.position.y); //Stops the player from moving past the "RightEdge" Object

		}


	

	shootTimer -= 10 * Time.deltaTime;

	if (shootTimer <= 0) {
		canShoot = true;
	}

	if (canShoot == true) {
			if (Input.GetKey (KeyCode.Y)) {
			Shoot1 ();
			canShoot = false;
			shootTimer = 20;
		}
			if (Input.GetKey (KeyCode.H)) {
			Shoot2 ();
			canShoot = false;
			shootTimer = 20;
		}
		if (Input.GetKey (KeyCode.N)) {
			Shoot3 ();
			canShoot = false;
			shootTimer = 20;
		}

	}
		Player2Animator.SetBool ("isFrozen2", isFrozen2);
	if (hit == true) {
			freezeTimer -= 1.75f * Time.deltaTime;
			if (freezeTimer > 0 && freezeTimer < 5) {
				freeze = true;
				isFrozen2 = true;
				isRunning2 = false;
				isJumping2 = false;
			}
			if (freezeTimer < 0) {
				freeze = false;
				freezeTimer = 5;
				hit = false;
				isFrozen2 = false;
			}

		}

		if (p2win == true) {
			HUD.SetActive (true);
			freeze = true;
		}

}


	void Shoot1 () {
		Vector2 firePointPosition = new Vector2 (firePointP.position.x, firePointP.position.y);
		Vector2 firedirectionPositionP = new Vector2 (firedirection1P.position.x, firedirection1P.position.y);
		//RaycastHit2D hit = Physics2D.Raycast (firePointPosition, firedirectionPositionP-firePointPosition, 1000, whatToHitP);
		GameObject bulletS = Instantiate (BulletSpritePrefab, firePointP.position, firePointP.rotation);
		bulletS.GetComponent<BulletScript> ().myCaster = this.gameObject;
		bulletS.GetComponent<Rigidbody2D>().AddForce ((firedirectionPositionP-firePointPosition).normalized*10, ForceMode2D.Impulse);
	
}
	void Shoot2 () {
		Vector2 firePointPosition = new Vector2 (firePointP.position.x, firePointP.position.y);
		Vector2 firedirectionPosition2P = new Vector2 (firedirection2P.position.x, firedirection2P.position.y);
		//RaycastHit2D hit = Physics2D.Raycast (firePointPosition, firedirectionPosition2P-firePointPosition, 1000, whatToHitP);
		GameObject bulletS = Instantiate (BulletSpritePrefab, firePointP.position, firePointP.rotation);
		bulletS.GetComponent<BulletScript> ().myCaster = this.gameObject;
		bulletS.GetComponent<Rigidbody2D>().AddForce ((firedirectionPosition2P-firePointPosition).normalized*10, ForceMode2D.Impulse);
	
}
	void Shoot3 () {
		Vector2 firePointPosition = new Vector2 (firePointP.position.x, firePointP.position.y);
		Vector2 firedirectionPosition3P = new Vector2 (firedirection3P.position.x, firedirection3P.position.y);
		//RaycastHit2D hit = Physics2D.Raycast (firePointPosition, firedirectionPosition3P-firePointPosition, 1000, whatToHitP);
		GameObject bulletS = Instantiate (BulletSpritePrefab, firePointP.position, firePointP.rotation);
		bulletS.GetComponent<BulletScript> ().myCaster = this.gameObject;
		bulletS.GetComponent<Rigidbody2D>().AddForce ((firedirectionPosition3P-firePointPosition).normalized*10, ForceMode2D.Impulse);
}
	void OnTriggerEnter2D(Collider2D otherObjectT) {
		CollisionDetection (otherObjectT.gameObject);
		if (otherObjectT.CompareTag ("PowerUp")) {
			Pickup2 ();
			Destroy (otherObjectT);
		}
		if (otherObjectT.CompareTag ("Speed")) {
			PickupSpeed2 ();
			Destroy (otherObjectT);
		}
		if (otherObjectT.CompareTag ("Splat")) {
			PickupSplat2 ();
			Destroy (otherObjectT);
		}
	}
	void OnCollisionEnter2D(Collision2D otherObjectC){
		Debug.Log (hit);
		CollisionDetection (otherObjectC.gameObject);
	}
	void CollisionDetection (GameObject otherObjectD) {
		if (otherObjectD.tag == "Bullet" && otherObjectD.GetComponent<BulletScript>().myCaster != this.gameObject) {
			Destroy (otherObjectD);
			hit = true;
			//Debug.Log (hit);
		}
	}
	void Pickup2 () {
		pickupTimer2 = -5;
		MainCamera1.transform.Rotate (0, 0, 180);
		//Destroy (gameObject);
	}
	void PickupSpeed2 () {
		speedTimer2 = -5;
	}
	void PickupSplat2 () {
		splatTimer2 = -5;
		Debug.Log ("PIcked up splat");
	}
}
