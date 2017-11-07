using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
//// Module: BuildingDeletion
/// Team: Client
/// Description: For Testing, not using in general
/// Author: 
///	 Name: Dongwon(Shawn) Kim   Date: 2017-10-09
/// Modified by:	
///	 Name: N/A   Change: N/A 				Date: N/A
/// Based on:  N/A
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
