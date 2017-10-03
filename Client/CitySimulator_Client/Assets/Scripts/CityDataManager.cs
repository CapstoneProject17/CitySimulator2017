using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 	Not used 
 **/
public class CityDataManager : MonoBehaviour {

	public int population = 1000;
	public int size_x = 10;
	public int size_y = 10;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int getSizeX (){
		return size_x;
	}

	public int getSizeY (){
		return size_y;
	}

	public int getPopulation (){
		return population;
	}

}
