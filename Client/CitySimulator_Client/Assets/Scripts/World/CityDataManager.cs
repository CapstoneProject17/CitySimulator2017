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
///  Name: Lancelei Herradura	Change: Adding walkable path Date: 2017-10-31
///	 Name: Dongwon(Shawn) Kim   Change:	gridforTest	 		 Date: 2017-11-13
/// Based on:  N/A
/// </summary>
public class CityDataManager : MonoBehaviour {

	// switch for testing
	public bool turnOnTestGrid;

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

	// grid map of the walkable path
	private bool[][] path;

	/// <summary>
	/// Gets or sets the path.
	/// </summary>
	/// <value>The path.</value>
	public bool [][] Path {
		get {
			return path;
		}
		set {
			path = value;
		}
	}
	
	/// <summary>
	// Awake this instance.
	/// </summary>
	void Awake () {

		if(turnOnTestGrid){ // if turned on for test, initiate test grid
			initiateGridForTest();
		} else {  // else general grid
			initiateGrid ();
		}
		
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

		// initalize 2d array
		for (int x = 0; x < grid.Length; x++) {
			grid [x] = new int[size_z]; 
		}

		// iterates grid and assign the zone
		for(int x = 0; x < size_x; x++){
			for (int z = size_z -1; z >= 0; z--) {
				grid [x][z] = Random.Range (0, 4);

				if(grid[x][z] == 2)
					grid[x][z] = 0;
			}
		}
	}


	/// <summary>
	/// Initiate grid for test
	/// </summary>
	public void initiateGridForTest(){		

		//hard coded grid for testing
	   	int[] arr1 	 = 	new [] { 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 3, 3, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr2 	 = 	new [] { 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 3, 3, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr3 	 = 	new [] { 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 3, 3, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr4 	 = 	new [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr5 	 = 	new [] { 0, 1, 3, 3, 3, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr6 	 = 	new [] { 0, 1, 3, 3, 3, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr7 	 = 	new [] { 0, 1, 3, 3, 3, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr8 	 = 	new [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr9 	 = 	new [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr10	 =	new [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr11	 =	new [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr12	 =	new [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr13	 =	new [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr14	 =	new [] { 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr15	 =	new [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr16	 =	new [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr17	 =	new [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr18	 =	new [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr19	 =	new [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr20	 =	new [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr21	 =	new [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr22	 =	new [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr23	 =	new [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr24	 =	new [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 3, 3, 3, 1, 1};
		int[] arr25	 =	new [] { 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1};


		// initializing and assigning arrays
		grid = new[] {
			arr1,
			arr2,
			arr3,
			arr4,
			arr5,
			arr6,
			arr7,
			arr8,
			arr9,
			arr10,
			arr11,
			arr12,
			arr13,
			arr14,
			arr15,
			arr16,
			arr17,
			arr18,
			arr19,
			arr20,
			arr21,
			arr22,
			arr23,
			arr24,
			arr25
		};

		// set the size to fit into array of
		size_x = grid.GetLength(0);
		size_z = grid[0].GetLength(0);

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
