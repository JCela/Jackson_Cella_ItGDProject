using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Script : MonoBehaviour {

	SpriteRenderer player;
	Rigidbody2D playerRigid;
    Collider2D playerCollider;
	public Animator Player1Animator;


	public GameObject HUD;
	GameObject MainCamera2;
	public GameObject ScreenSplat2;

	public LayerMask whatToHit;

	//public Text dashTimerText;
	public Sprite victorypng;

	float jumpForce = 14;
	float speed = 0.1f;
	float shootTimer = 20;
	float freezeTimer = 2;
	float dash = 0.25f;
	float dashspeed = .5f;
	float dashCounter = -5;
	float canDashTimer = 0;

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

	public GameObject BulletSpritePrefab;

	private float pickupTimer1 = 2;
	private float speedTimer1 = 2;
	private float splatTimer1 = 2;




	// Use this for initialization

	void Start () {

		MainCamera2 = GameObject.Find ("Main Camera 2");
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
		dashCounter += 1*Time.deltaTime;

		canDashTimer -= 1 * Time.deltaTime;
		pickupTimer1 = pickupTimer1 + 1 * Time.deltaTime;
		speedTimer1 = speedTimer1 + 1 * Time.deltaTime;
		splatTimer1 = splatTimer1 + 1 * Time.deltaTime;

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
				playerRigid.velocity = new Vector2 (playerRigid.velocity.x, jumpForce+5); // Jump mechanic, checks to see if player is Grounded to jump
				grounded = false;
			}
		}
		//if (Player1Animator.GetAnimatorTransitionInfo (0).IsName ("RunningAnimation1") == false) {
		//	Player1Animator.Play ("IdleAnimation");
		//}

		if (freeze == false && speedTimer1 >0 && canDashTimer <0) {
			//Debug.Log ("Moving"+ canDashTimer);


			if (Input.GetKey (KeyCode.A)) {
				this.GetComponent<Transform> ().Translate (new Vector3 (-speed, 0));


			}
			if (Input.GetKey (KeyCode.D)) {
				this.GetComponent<Transform> ().Translate (new Vector3 (speed, 0));
			} 




		//	if (Input.GetKeyUp (KeyCode.D)) {
		//	Player1Animator.Play ("IdleAnimation");
			
		//	}
			if (Input.GetKeyDown (KeyCode.W) && grounded) {
				playerRigid.velocity = new Vector2 (playerRigid.velocity.x, jumpForce); // Jump mechanic, checks to see if player is Grounded to jump
				grounded = false;	
			}

		}
		bool isRunning = false;
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

		Player1Animator.SetBool ("isRunning", isRunning);

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
			
			//if (grounded == false) {
			//	playerCollider.isTrigger = true;
			//} else {
			//	playerCollider.isTrigger = false;
			//}

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

		if (hit == true) {
			freezeTimer -= 1.75f * Time.deltaTime;
			if (freezeTimer > 0 && freezeTimer < 2) {
				freeze = true;
			}
			if (freezeTimer < 0) {
				freeze = false;
				freezeTimer = 2;
				hit = false;
			}

		}

		//dashTimerText.text = "Dash: " + dashCounter;

		//*******Winning*********//

		if (p1win == true) {
			HUD.SetActive (true);
			freeze = true;
		}
	}


	void Shoot1 () {
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		Vector2 firedirectionPosition = new Vector2 (firedirection1.position.x, firedirection1.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, firedirectionPosition-firePointPosition, 1000, whatToHit);
		GameObject bulletS = Instantiate (BulletSpritePrefab, firePoint.position, firePoint.rotation);
		bulletS.GetComponent<BulletScript> ().myCaster = this.gameObject;
		bulletS.GetComponent<Rigidbody2D>().AddForce ((firedirectionPosition-firePointPosition).normalized*10, ForceMode2D.Impulse);
		if (hit.collider != null) {
		}
	}
	void Shoot2 () {
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		Vector2 firedirectionPosition2 = new Vector2 (firedirection2.position.x, firedirection2.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, firedirectionPosition2-firePointPosition, 1000, whatToHit);
		GameObject bulletS = Instantiate (BulletSpritePrefab, firePoint.position, firePoint.rotation);
		bulletS.GetComponent<BulletScript> ().myCaster = this.gameObject;
		bulletS.GetComponent<Rigidbody2D>().AddForce ((firedirectionPosition2-firePointPosition).normalized*10, ForceMode2D.Impulse);
		if (hit.collider != null) {
		}
	}
	void Shoot3 () {
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		Vector2 firedirectionPosition3 = new Vector2 (firedirection3.position.x, firedirection3.position.y);
		//RaycastHit2D hit = Physics2D.Raycast (firePointPosition, firedirectionPosition3-firePointPosition, 1000, whatToHit);
		GameObject bulletS = Instantiate (BulletSpritePrefab, firePoint.position, firePoint.rotation);
		bulletS.GetComponent<BulletScript> ().myCaster = this.gameObject;
		bulletS.GetComponent<Rigidbody2D>().AddForce ((firedirectionPosition3-firePointPosition).normalized*10, ForceMode2D.Impulse);
		//if (hit.collider != null) {
		//	Debug.Log ("We Hit" + hit.collider.name);
		//}

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
		//Destroy
	}
	void PickupSpeed1 () {
		speedTimer1 = -5;
	}
	void PickupSplat1 () {
		splatTimer1 = -5;
		Debug.Log ("PIcked up splat");
	}

}
