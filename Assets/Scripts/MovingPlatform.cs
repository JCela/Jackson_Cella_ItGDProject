using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public enum Modes
	{
		PING_PONG,
		LOOP
	}

	public Modes mode;

	public bool isMoving;
	public float speed;
	PointsHolder pointsHolder;
	Transform[] allPoints;
	public Rigidbody2D rb2d;

	int direction = 1;

	void Start()
	{
		pointsHolder = transform.Find ("Points").GetComponent<PointsHolder> ();
		//rb2d = GetComponent<RigidBody2D>();
		allPoints = pointsHolder.getAllPoints ();


		StartCoroutine (moveAround ());
	}

	IEnumerator moveAround()
	{
		transform.position = allPoints [0].position;

		int index = 1;

		while (isMoving) {

			while(rb2d.transform.position != allPoints[index].position)
			{
				rb2d.transform.position = Vector2.MoveTowards(rb2d.transform.position,allPoints[index].position,speed*Time.deltaTime);
				yield return null;
			}

			if(mode == Modes.PING_PONG)
			if(index == allPoints.Length-1 || index == 0)
				direction *=-1;

			if(mode == Modes.LOOP)
			if(index == allPoints.Length-1)
				index = -1;

			index+= direction;
			yield return null;
		}
	}


}
