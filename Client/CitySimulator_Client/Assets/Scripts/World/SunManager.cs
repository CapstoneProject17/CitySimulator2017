using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Module: Sun manager
/// Team: Client
/// Description: Move, rotate, disappear sun by time
/// Author: 
///	 Name: Dongwon(Shawn) Kim    Date: 2017-10-31
/// Modified by:	
///	 Name: N/A   Change: N/A 						Date: N/A
/// Based on:  
///	 https://www.youtube.com/watch?v=DmhSWEJjphQ
/// </summary>
public class SunManager : MonoBehaviour {

	public float timeMagnitude;

	// Use this for initialization
	void Start () {
		timeMagnitude = 50f;
	}

	// Update is called once per frame
	void Update () {
		transform.RotateAround (Vector3.zero, Vector3.right, timeMagnitude * Time.deltaTime);
		transform.LookAt (Vector2.zero);
	}
}
