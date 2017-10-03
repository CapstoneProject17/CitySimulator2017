using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * GridColor that color sprite renderer to represent zone
 * 
 * Author: 		Dongwon(Shawn) Kim
 * Date(Cr): 	2017-09-18
 * Date(Mo):	2017-09-18 
 * Reference:
 * https://docs.unity3d.com/ScriptReference/Material-color.html
 **/
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
