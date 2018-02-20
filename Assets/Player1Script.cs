using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Script : MonoBehaviour {

	SpriteRenderer player;
	Rigidbody2D playerRigid;
	Collider2D playerCollider;

	public LayerMask whatToHit;

	public Text dashTimerText;

	float jumpForce = 16;
	float speed = 0.1f;
	float shootTimer = 20;
	float freezeTimer = 2;
	float dash = 0.25f;
	float dashCounter = 5;

	bool hit = false;
	bool canShoot = false;
	bool grounded = true;
	bool freeze = false;

	Transform firePoint;
	Transform leftEdge;
	Transform rightEdge;
	Transform middleEdge;
	Transform groundDetector;
	Transform firedirection1;
	Transform firedirection2;
	Transform firedirection3;

	public GameObject BulletSpritePrefab;



	// Use this for initialization
	void Start () {
		player = this.GetComponent<SpriteRenderer> ();

		playerRigid = this.GetComponent<Rigidbody2D> ();

		playerCollider = this.GetComponent<Collider2D> ();

		groundDetector = transform.Find("GroundDetector");

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
		dashCounter -= 1*Time.deltaTime;

		grounded = Physics2D.Linecast(transform.position, groundDetector.position, 1 << LayerMask.NameToLayer("Ground")); //uses linecast to determine if the player is grounded
		if (freeze == false) {


			if (Input.GetKey (KeyCode.A)) {
				this.GetComponent<Transform> ().Translate (new Vector3 (-speed, 0));
				if (Input.GetKey (KeyCode.LeftShift) && dashCounter < 0) {
					//playerRigid.velocity = new Vector2 (-12, playerRigid.velocity.y);
					playerRigid.AddForce (Vector2.left*9, ForceMode2D.Impulse);
					dashCounter = 5;
				}
			}
			if (Input.GetKey (KeyCode.D)) {
				this.GetComponent<Transform> ().Translate (new Vector3 (speed, 0));
				if (Input.GetKey (KeyCode.LeftShift) && dashCounter < 0) {
					//playerRigid.velocity = new Vector2 (-12, playerRigid.velocity.y);
					playerRigid.AddForce (Vector2.right*9, ForceMode2D.Impulse);
					dashCounter = 5;
				}
			}
			if (Input.GetKeyDown (KeyCode.W) && grounded) {
				//playerRigid.AddForce (Vector2.up*jumpForce, ForceMode2D.Impulse);
				playerRigid.velocity = new Vector2 (playerRigid.velocity.x, jumpForce); // Jump mechanic, checks to see if player is Grounded to jump
				grounded = false;
			}
		}
			if (grounded == false) {
				playerCollider.isTrigger = true;
			} else {
				playerCollider.isTrigger = false;
			}

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

		dashTimerText.text = "Dash: " + dashCounter;
	}


	void Shoot1 () {
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		Vector2 firedirectionPosition = new Vector2 (firedirection1.position.x, firedirection1.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, firedirectionPosition-firePointPosition, 1000, whatToHit);
		GameObject bulletS = Instantiate (BulletSpritePrefab, firePoint.position, firePoint.rotation);
		bulletS.GetComponent<BulletScript> ().myCaster = this.gameObject;
		bulletS.GetComponent<Rigidbody2D>().AddForce ((firedirectionPosition-firePointPosition).normalized*10, ForceMode2D.Impulse);
		if (hit.collider != null) {
			Debug.Log ("We Hit" + hit.collider.name);
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
			Debug.Log ("We Hit" + hit.collider.name);
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
