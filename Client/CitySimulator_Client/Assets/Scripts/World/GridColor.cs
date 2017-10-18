using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Module: GridColor
/// Team: Client
/// Description: Handle the all the data of the city, for now it has been used
/// Author: 
///	 Name: Dongwon(Shawn) Kim    Date: 2017-09-18
/// Modified by:	
///	 Name: N/A   Change: N/A	Fix bug Date: N/A
/// Based on:  N/A
/// https://docs.unity3d.com/ScriptReference/Material-color.html
/// </summary>
public class GridColor : MonoBehaviour {
	
	//	public Color colorStart = Color.red;
//	public Color colorEnd = Color.green;
//	public float duration = 1.0F;
//	public float minRange = 1.0f;
//	public float maxRange = 2.0f;

	public Renderer rend;				// renderer object for coloring
	public int colorIndex = 0;			// index of color of itself


	public Color32[] colors = {			// the set of color gonna be use
		Color.grey,						// 0: Street & Path 
		Color.red,						// 1: Residential
		Color.yellow,					// 2: Commercial
		Color.green						// 3: Industry
	};


	/**
	 * 
	 **/
	void Start() {

		// renderer reference to itself
		rend = GetComponent<Renderer>(); 
	}

	//for fun
//	void Update() {
//		float lerp = Mathf.PingPong(Time.time, duration) / Random.Range (minRange, maxRange);
//		rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);
//	}


	void Update(){

		//update color based on the index
		rend.material.color = colors [colorIndex];
	}
}
