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
///	 Name: Dongwon(Shawn) Kim   Change:	Start to use		 Date: 2017-10-18
/// Based on:  N/A
/// </summary>
public class CityDataManager : MonoBehaviour {

	// population of the city
	public int population;
	// x number of grids in horizontal
	public int size_x;
	// y number of grids in vertical
	public int size_z;
	// grid 2D map: information of grids
	public int[][] grid;
	public bool forPathFind;

	void Awake () {
		size_x = 49;
		size_z = 36;
		population = 1000;

		forPathFind = true;

		initialGrid ();

		if (forPathFind) {
			assignAllZoneToGrid ();
		} 
	}

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
	public int getSizeZ (){
		return size_z;
	}

	/// <summary>
	/// Gets the population.
	/// </summary>
	/// <returns>The population.</returns>
	public int getPopulation (){
		return population;
	}

	public int getIndexOfXZ(int x, int z){
		if (x > size_x && z > size_z) {
			return -1;
		}
		return grid[x][z];
	}
	public void initialGrid(){
		grid = new int[size_x][];

		for (int x = 0; x < grid.Length; x++) {
			grid [x] = new int[size_z]; 
		}
	}

	public void assignAllZoneToGrid(){
		for(int x = 0; x < size_x; x++){
			for (int z = size_z -1; z >= 0; z--) {
				grid [x][z] = Random.Range(0,4);
			}
		}
	}

}
