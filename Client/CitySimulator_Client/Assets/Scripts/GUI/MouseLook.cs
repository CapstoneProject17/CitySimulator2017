using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mouse look.
/// Purspose: Controls the camera's direction using Mouse coordinates.
/// Source: http://answers.unity3d.com/questions/29741/mouse-look-script.html
/// Author:  AndyP-123
/// Date: 01.31.16.
/// Updated By: Lancelei Herradura
/// Updated At: 09.26.17.
/// </summary>
public class MouseLook : MonoBehaviour {

	public float mouseSensitivity = 50.0f;
	public float clampAngle = 80.0f;

	private float rotY = 0.0f; // rotation around the up/y axis
	private float rotX = 0.0f; // rotation around the right/x axis

	void Start ()
	{
		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;
	}

	void Update ()
	{
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = -Input.GetAxis("Mouse Y");

		rotY += mouseX * mouseSensitivity * Time.deltaTime;
		rotX += mouseY * mouseSensitivity * Time.deltaTime;

		rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

		Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
		transform.rotation = localRotation;
	}
}
