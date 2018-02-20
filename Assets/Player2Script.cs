using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Script : MonoBehaviour {

	SpriteRenderer player;
	float speed = 0.1f;
	Rigidbody2D playerRigid;
	float jumpForce = 16;
	Collider2D playerCollider;
	float shootTimer = 20;
	float freezeTimer = 2;
	float dashCounter = 5;

	bool hit = false;
	bool freeze = false;
	public LayerMask whatToHitP;


	bool canShoot = false;

	Transform groundDetector;

	Transform firePointP;
	Transform leftEdge;
	Transform rightEdge;
	Transform middleEdge;
	Transform firedirection1P;
	Transform firedirection2P;
	Transform firedirection3P;

	bool groundedtwo = true;

	public GameObject BulletSpritePrefab;


	// Use this for initialization
	void Start () {
		player = this.GetComponent<SpriteRenderer> ();

		playerRigid = this.GetComponent<Rigidbody2D> ();

		playerCollider = this.GetComponent<Collider2D> ();

		groundDetector = transform.Find("GroundDetector2");

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

		groundedtwo = Physics2D.Linecast(transform.position, groundDetector.position, 1 << LayerMask.NameToLayer("Ground")); //uses linecast to determine if the player is grounded

		dashCounter -= 1 * Time.deltaTime;

		if (freeze == false) {
			if (Input.GetKey (KeyCode.J)) {
				this.GetComponent<Transform> ().Translate (new Vector3 (-speed, 0));
				if (Input.GetKey (KeyCode.Semicolon) && dashCounter < 0) {
					//playerRigid.velocity = new Vector2 (-12, playerRigid.velocity.y);
					playerRigid.AddForce (Vector2.left*9, ForceMode2D.Impulse);
					dashCounter = 5;
				}
			}
			if (Input.GetKey (KeyCode.L)) {
				this.GetComponent<Transform> ().Translate (new Vector3 (speed, 0));
				if (Input.GetKey (KeyCode.Semicolon) && dashCounter < 0) {
					//playerRigid.velocity = new Vector2 (-12, playerRigid.velocity.y);
					playerRigid.AddForce (Vector2.right*9, ForceMode2D.Impulse);
					dashCounter = 5;
				}
			}

			if (Input.GetKeyDown (KeyCode.I) && groundedtwo) {
				//playerRigid.AddForce (Vector2.up*jumpForce, ForceMode2D.Impulse);
				playerRigid.velocity = new Vector2 (playerRigid.velocity.x, jumpForce); // Jump mechanic, checks to see if player is Grounded to jump
				groundedtwo = false;
			}
		}
		if (groundedtwo == false) {
			playerCollider.isTrigger = true;
		} else {
			playerCollider.isTrigger = false;
		}

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
		Debug.Log ("DebugTrigger");
	}
	void OnCollisionEnter2D(Collision2D otherObjectC){
		CollisionDetection (otherObjectC.gameObject);
		Debug.Log ("DebugEnter");
	}
	void CollisionDetection (GameObject otherObjectD) {
		if (otherObjectD.tag == "Bullet" && otherObjectD.GetComponent<BulletScript>().myCaster != this.gameObject) {
			Debug.Log ("Bullet");
			Destroy (otherObjectD);
			hit = true;
		}
	}
}
