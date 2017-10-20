using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Building deletion.
/// Author: Dongwon(Shawn) Kim
/// 
/// </summary>
public class BuildingDeletion : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		
		Destroy(GameObject.Find ("Residential Buildings 002"));
		Destroy(GameObject.Find ("Residential Buildings 003"));
		Destroy(GameObject.Find ("Residential Buildings 004"));
		Destroy(GameObject.Find ("Residential Buildings 005"));
		Destroy(GameObject.Find ("Residential Buildings 006"));
		Destroy(GameObject.Find ("Residential Buildings 007"));
		Destroy(GameObject.Find ("Residential Buildings 008"));
		Destroy(GameObject.Find ("Residential Buildings 009"));
		Destroy(GameObject.Find ("Residential Buildings 010"));
	}
}
