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
///	 Name: Dongwon(Shawn) Kim   Change:	Start to use		 Date: 2017-10-17
///	 Name: Dongwon(Shawn) Kim   Change:	bug fix		 		 Date: 2017-10-18
/// Based on:  N/A
/// </summary>
public class CityDataManager : MonoBehaviour {

	// population of the city
	private int population = 1000;

	/// <summary>
	/// Gets the population.
	/// </summary>
	/// <returns>The population.</returns>
	public int Population{
		get {
			return  population;
		}
		set {
			population = value;
		}
	}

	// x number of grids in horizontal
	private int size_x = 50;

	/// <summary>
	/// Gets the size x.
	/// </summary>
	/// <returns>The size x.</returns>
	public int Size_x{
		get{
			return size_x;
		}
		set{
			size_x = value;
		}
	}

	// z number of grids in vertical
	private int size_z = 50;

	/// <summary>
	/// Gets the size z.
	/// </summary>
	/// <returns>The size z.</returns>
	public int Size_z{
		get{
			return size_z;
		}
		set{
			size_z = value;
		}
	}


	// grid map contains the index of the zone
	private int[][] grid;

	/// <summary>
	/// Gets or sets the grid.
	/// </summary>
	/// <value>The grid.</value>
	public int[][] Grid{
		get{
			return grid;
		}
		set{
			grid = value;
		}
	}

	// Awake this instance.
	void Awake () {
		initiateGrid ();
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update(){
		/// Will be used in the future
	}

	/// <summary>
	/// Initials the grid.
	/// </summary>
	public void initiateGrid(){
		grid = new int[size_x][];

		for (int x = 0; x < grid.Length; x++) {
			grid [x] = new int[size_z]; 
		}

		for(int x = 0; x < size_x; x++){
			for (int z = size_z -1; z >= 0; z--) {
				grid [x][z] = Random.Range (0, 4);
			}
		}
	}

	/// <summary>
	/// Gets the index of X.
	/// </summary>
	/// <returns>The index of X.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public int getIndexOfXZ(int x, int z){

		if (x > size_x && z > size_z) {
			return -1;
		}

		return grid[x][z];
	}

}
