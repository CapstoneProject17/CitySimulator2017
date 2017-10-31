using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Module: CameraMovement
/// Team: Client
/// Description: Controls movement of the camera using keys.
/// Author:
/// 	Name: Lancelei Herradura	Date: 2017-09-26
/// Based on: 
/// 	Tokars's solution to camera movement
/// 	Link: http://answers.unity3d.com/questions/548794/how-to-move-a-camera-only-using-the-arrow-keys.html
/// 			
/// </summary>
public class CameraMovement : MonoBehaviour {
	// camera movement speed
	private static float speed = 10f;
	// position before moving
	private static Vector3 oldPos;
	// aerial view enabler
	private static bool aerial;		

	/// <summary>
	/// Description: Initialize camera movement. User initially not in aerial mode.
	/// Author:
	/// 	Name: Lancelei Herradura	Date: 2017-09-26
	/// </summary>
	void Start () {
		aerial = false;	
	}
	
	/// <summary>
	/// Description: Update is called once per frame.
	/// Author:
	/// 	Name: Lancelei Herradura	Date: 2017-09-26
	/// </summary>
	void Update () {

		if (!Input.GetKey (KeyCode.Space)) {
			
			// Make the camera move faster
			if(Input.GetKey(KeyCode.LeftShift)) {
				speed = 40f;
			} else {
				speed = 10f;
			}	
				
			// Disable aerial mode
			if (aerial) {
				transform.position = oldPos; 
				aerial = false;
			}

			// moves Camera right
			if (Input.GetKey (KeyCode.D)) {
				transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
			}
			// moves Camera left
			if (Input.GetKey (KeyCode.A)) {
				transform.Translate (new Vector3 (-speed * Time.deltaTime, 0, 0));
			}
			// moves Camera down
			if (Input.GetKey (KeyCode.Q)) {
				transform.Translate (new Vector3 (0, -speed * Time.deltaTime, 0));
			}
			// moves Camera up
			if (Input.GetKey (KeyCode.E)) {
				transform.Translate (new Vector3 (0, speed * Time.deltaTime, 0));
			}
			// moves Camera forward
			if (Input.GetKey (KeyCode.W)) {
				transform.Translate (new Vector3 (0, 0, speed * Time.deltaTime));
			}
			// moves Camera backward
			if (Input.GetKey (KeyCode.S)) {
				transform.Translate (new Vector3 (0, 0, -speed * Time.deltaTime));
			}
		} else {
			if (!aerial)
				oldPos = transform.position;
			transform.position = new Vector3 (25, 120, 0);
			aerial = true;
		}
		
	}
}
