using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Module: GridCreation
/// Team: Client
/// Description: creates 3D grid based on data from CityDataManager
/// Author: 
///	 Name: Dongwon(Shawn) Kim    Date: 2017-09-18
/// Modified by:	
///	 Name: Dongwon(Shawn) Kim   Change:	Fix bug 						Date: 2017-09-19
///  Name: Dongwon(Shawn) Kim   Change:	adding data by CityDataManager  Date: 2017-10-17
///  Name: Dongwon(Shawn) Kim   Change:	refactoring 					Date: 2017-10-18
/// Based on:  
/// 	http://answers.unity3d.com/questions/718778/trying-to-create-a-grid.html
/// 	https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
/// 	http://answers.unity3d.com/questions/25352/add-texture-to-gameobject-through-code.html
/// 	https://www.youtube.com/watch?v=810UVUWGlWw
/// </summary>
public class GridCreation : MonoBehaviour {

	public Transform cellPrefab;
	private Vector3 size;
	public Random randZone;
	private CityDataManager cityDataManager;
	public int gap;

	// Use this for initialization
	void Start () {
		cityDataManager = this.GetComponent<CityDataManager> ();

		size.x = cityDataManager.getSizeX();
		gap = 8;
		Debug.Log (size.x.ToString());
		Debug.Log (size.y.ToString());
		Debug.Log (size.z.ToString());
		size.z = cityDataManager.getSizeZ();
		CreateGrid ();
	}

	void Update(){
	}

	/// <summary>
	/// Creates the grid.
	/// </summary>
	void CreateGrid(){

//		Debug.Log (cellPrefab.localScale.x +", " + cellPrefab.localScale.z);
		Debug.Log (size.x.ToString());
		Debug.Log (size.y.ToString());
		Debug.Log (size.z.ToString());

		for(int x = 0; x < size.x; x++){
			for(int z = 0; z < size.z; z++){

				// getting random number for zone( its temporally used for prototype)
//				cellPrefab.GetChild (0).GetComponent<TextMesh> ().text = (Random.Range (0, 4)).ToString();


				// asign information to grid from cityDataManager
				cellPrefab.GetChild (1).GetComponent<TextMesh> ().text = "(" + x + ", " + z + ")";
				cellPrefab.GetChild (0).GetComponent<TextMesh> ().text = cityDataManager.getIndexOfXZ(x, z).ToString();

				cellPrefab.tag = "plane";

					
				// set color index to GridColor to color the grid
				cellPrefab.GetComponent<GridColor> ().colorIndex = int.Parse(cellPrefab.GetChild (0).GetComponent<TextMesh> ().text);

				// set the coordinate
				float xCoordinate = x + (cellPrefab.localScale.x * x) * gap;
				float zCoordinate = z + (cellPrefab.localScale.z * z) * gap;

				//Creating and locating each cell of grid
			 	Instantiate(cellPrefab, new Vector3(xCoordinate, 0, zCoordinate), Quaternion.identity);
				
			}
		}
	}

	void SetCoordinate(){
	}

	void SetZone() {
		
	}

}