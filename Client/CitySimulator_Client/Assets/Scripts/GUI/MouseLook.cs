using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Module: Mouse look.
/// Team: Client
/// Description: Controls the camera's direction using Mouse coordinates.
/// Author:
///	 Name: Lancelei Herradura    Date: 01.31.17.
/// Modified by:	
///	 Name: Dongwon(Shawn) Kim   Change:	addin multi key for roatiting  Date: 2017-10-17
/// Based on:
///  http://answers.unity3d.com/questions/29741/mouse-look-script.html
/// </summary>
public class MouseLook : MonoBehaviour {

	//sensitivity of mouse
	public float mouseSensitivity = 40.0f;
	//clamp angle for viewport
	public float clampAngle = 80.0f;
 	
 	// rotation around the up/y axis
	private float rotY = 0.0f;
	// rotation around the right/x axis
	private float rotX = 0.0f;

	void Start ()
	{
		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;
	}

	void Update ()
	{
		if(Input.GetKey(KeyCode.LeftControl)){
			float mouseX = Input.GetAxis("Mouse X");
			float mouseY = -Input.GetAxis("Mouse Y");

			rotY += mouseX * mouseSensitivity * Time.deltaTime;
			rotX += mouseY * mouseSensitivity * Time.deltaTime;

			rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

			Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
			transform.rotation = localRotation;
		}
	}
}
