using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Module: CityDataManager
/// Team: Client
/// Description: Handle the all the data of the city, for now it has been used
/// Author:
///	 Name: Dongwon(Shawn) Kim    Date: 2017-09-11
/// Modified by:	
///	 Name: Dongwon(Shawn) Kim   Change:	Fix bug Date: 2017-09-12
/// Based on:  N/A
/// </summary>
public class CityDataManager : MonoBehaviour {

	// population of the city
	public int population = 1000;
	// x number of grids in horizontal
	public int size_x = 10;
	// y number of grids in vertical
	public int size_y = 10;

	// Use this for initialization
	void Start () {
	}


	/// <summary>
	/// Gets the size x.
	/// </summary>
	/// <returns>The size x.</returns>
	public int getSizeX (){
		return size_x;
	}

	/// <summary>
	/// Gets the size y.
	/// </summary>
	/// <returns>The size y.</returns>
	public int getSizeY (){
		return size_y;
	}

	/// <summary>
	/// Gets the population.
	/// </summary>
	/// <returns>The population.</returns>
	public int getPopulation (){
		return population;
	}

}
