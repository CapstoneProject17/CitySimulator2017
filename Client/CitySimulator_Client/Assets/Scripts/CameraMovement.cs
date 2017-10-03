using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera movement.
/// Purpose: Controls movement of the camera using keys.
/// Author: Lancelei Herradura
/// Date: 09.26.17.
/// </summary>
public class CameraMovement : MonoBehaviour {

	// Camera movement speed
	private static float speed = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// moves Camera right
		if(Input.GetKey(KeyCode.D))
		{
			transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
		}
		// moves Camera left
		if(Input.GetKey(KeyCode.A))
		{
			transform.Translate(new Vector3(-speed * Time.deltaTime,0,0));
		}
		// moves Camera down
		if(Input.GetKey(KeyCode.Q))
		{
			transform.Translate(new Vector3(0,-speed * Time.deltaTime,0));
		}
		// moves Camera up
		if(Input.GetKey(KeyCode.E))
		{
			transform.Translate(new Vector3(0,speed * Time.deltaTime,0));
		}
		// moves Camera forward
		if (Input.GetKey (KeyCode.W)) {
			transform.Translate (new Vector3 (0, 0, speed * Time.deltaTime));
		}
		// moves Camera backward
		if (Input.GetKey (KeyCode.S)) {
			transform.Translate (new Vector3 (0, 0, -speed * Time.deltaTime));
		}
		
	}
}
