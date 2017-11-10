using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Module: BuildingCreation
/// Team: Client
/// Description: Creating building depends on the grid information
/// Author: 
///	 Name: Dongwon(Shawn) Kim   Date: 2017-09-28
///  Name: Andrew Lam			Date: 2017-09-28
/// Modified by:	
///	 Name: Andrew Lam   Change: Change the reference of the building	Date: 2017-10-17
///	 Name: Shawn  Kim   Change: Append objects to parent 				Date: 2017-10-20
/// Based on:  N/A
/// https://docs.unity3d.com/ScriptReference/Material-color.html
/// </summary>
public class BuildingCreation : MonoBehaviour {

	public Transform building;
	public Transform industryBuilding;
	private GameObject[] planes;
	private Transform planeTransform;
	public GameObject buildingManager;

	// Use this for initialization
	void Start () {
		planeTransform = GameObject.Find("Plane(Clone)").transform;
		buildingManager = GameObject.Find ("BuildingManager");
		createBuilding ();
	}

	// Update is called once per frame
	void Update () {
	}

	// Create building model
	void createBuilding () {
		planes = GameObject.FindGameObjectsWithTag ("plane");
		int x = 0;
		int z = 0;

		building.localScale = planeTransform.localScale;
		building.localScale -= new Vector3 (0.15f, 0, 0.15f);

		industryBuilding.localScale = planeTransform.localScale;
		industryBuilding.localScale -= new Vector3 (0.15f, 0.30f, 0.30f);

		foreach(GameObject grid in planes) {
			Transform grid2 = grid.transform;
			// Residential building models
			if (grid2.GetChild(0).GetComponent<TextMesh>().text == "1") {
//				Debug.Log ("find 1: " + grid.transform.position.x + ", " + grid.transform.position.y);

				//Creating each cell of grid
				Instantiate (building,
							new Vector3 (grid.transform.position.x, 0, grid.transform.position.z),
							Quaternion.identity, 
							buildingManager.transform);
			}

			//Industrial building objects
			if (grid2.GetChild(0).GetComponent<TextMesh>().text == "3") {
//				Debug.Log ("find 3: " + grid.transform.position.x + ", " + grid.transform.position.y);

				//Creating each cell of grid
				Instantiate (industryBuilding,
					new Vector3 (grid.transform.position.x,
								grid.transform.position.y,
								grid.transform.position.z),
								Quaternion.identity,
								buildingManager.transform);
			}

			z++;
			x++;
		}

		//		if (plane.GetComponentInChildren<TextMesh> ().text == "1") {
		//			Instantiate (building);
		//		}
	}
}
