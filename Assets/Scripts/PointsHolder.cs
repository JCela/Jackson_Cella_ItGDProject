using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsHolder : MonoBehaviour {

	Transform[] allPoints;

	// Use this for initialization
	void Awake () {

		allPoints = new Transform[transform.childCount];

	}

	public Transform[] getAllPoints()
	{

		for (int i=0; i<allPoints.Length; i++) {
			allPoints[i] = transform.GetChild(i);
		}
	
	
		return allPoints;
	}

}
