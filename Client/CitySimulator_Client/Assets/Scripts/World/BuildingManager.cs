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
///	 Name: Andrew Lam   Change: create, update, destroy building funtions added 	Date: 2017-11-25
///	 Name: Shawn  Kim   Change: Append objects to parent 							Date: 2017-10-20
/// Based on:  N/A
///  https://docs.unity3d.com/ScriptReference/Material-color.html
///  https://docs.unity3d.com/ScriptReference/GameObject.html
///  https://docs.unity3d.com/ScriptReference/Resources.Load.html
/// </summary>
public class BuildingManager : MonoBehaviour {

	public GameObject buildingManager;

	private GameObject[] planes;

	private Transform planeTransform;

	public GameObject residential1;
	public GameObject residential2;
	public GameObject residential3;
	public GameObject commercial1;
	public GameObject commercial2;
	public GameObject commercial3;
	public GameObject industrial1;
	public GameObject industrial2;
	public GameObject industrial3;

	// Update builings
	void updateBuildings (string id) {

		// Find the building object with the matching id
		GameObject building = GameObject.Find( id + "(Clone)");
	}

	// Create new building to on given grid location
	public void createBuilding (string GUID, int x, int z, int type, int rating) {

		planes = GameObject.FindGameObjectsWithTag ("plane");
		GameObject go = findPlane(x,z);

		Debug.Log("Reciever Order: Create BUilding: " + GUID + " " + x +", " + z + " " + type + " " + rating);
		
		// Finds the type of the building. eg. Industrial, Residential, Commercial.
		switch(type) 
		{
			// Residental 
			case 1: 
				if(rating == 0) {
				residential1.name = GUID;
				instantiateBuilding (residential1,
									 go.transform.localPosition.x,
									 0,
									 go.transform.localPosition.z);
               	rotateBuilding(residential1, go);
				}

				if(rating == 1) {
				residential2.name = GUID;
				instantiateBuilding (residential2,
									 go.transform.localPosition.x,
									 0,
									 go.transform.localPosition.z);
				rotateBuilding(residential2, go);
				}

				if(rating == 2) {	
				residential3.name = GUID;
				instantiateBuilding (residential3,
									 go.transform.localPosition.x,
									 0,
									 go.transform.localPosition.z);
				rotateBuilding(residential3, go);
				}
			break; 

			// Commercial
			case 2:
				if(rating == 0) {
				commercial1.name = GUID;
				instantiateBuilding (commercial1,
									 go.transform.localPosition.x,
									 0,
									 go.transform.localPosition.z);
				rotateBuilding(commercial1, go);
				}

				if(rating == 1) {
				commercial2.name = GUID;
				instantiateBuilding (commercial2,
									 go.transform.localPosition.x,
									 0,
									 go.transform.localPosition.z);
				rotateBuilding(commercial2, go);
				}

				if(rating == 2) {	
				commercial3.name = GUID;
				instantiateBuilding (commercial3,
									 go.transform.localPosition.x,
									 0,
									 go.transform.localPosition.z);
				rotateBuilding(commercial3, go);
				}
			break;

			// Industrial
			case 3:
				if(rating == 0) {
				industrial1.name = GUID;
				instantiateBuilding (industrial1,
									 go.transform.localPosition.x,
									 0,
									 go.transform.localPosition.z);
				rotateBuilding(industrial1, go);
				}

				if(rating == 1) {
				industrial2.name = GUID;
				instantiateBuilding (industrial2,
									 go.transform.localPosition.x,
									 0,
									 go.transform.localPosition.z);
				rotateBuilding(industrial2, go);
				}

				if(rating == 2) {
				industrial3.name = GUID;
				instantiateBuilding (industrial3,
									 go.transform.localPosition.x,
									 0,
									 go.transform.localPosition.z);
				rotateBuilding(industrial3, go);
				}
			break;
		}
	}

	// Dispose building 
	public void disposeBuilding (string id) {

		GameObject building = GameObject.Find( id + "(Clone)");

		Destroy(building);
	}

	// initialize building models
	public void intializeBuildings () {
		planes = GameObject.FindGameObjectsWithTag ("plane");
		int x = 0;
		int z = 0;

		foreach(GameObject grid in planes) {

			// Residential building models
			if (grid.transform.GetChild(0).GetComponent<TextMesh>().text == "1") {
				
				//Creating each cell of grid
				instantiateBuilding(residential2, 
									grid.transform.position.x,
									grid.transform.position.y,
									grid.transform.position.z);
               	residential2.transform.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Models/Building/New_Models/ACs/ACR2") as RuntimeAnimatorController;
			}

			//Commercial building objects
			if (grid.transform.GetChild(0).GetComponent<TextMesh>().text == "3") {
								
				instantiateBuilding(commercial1, 
									grid.transform.position.x,
									grid.transform.position.y,
									grid.transform.position.z);
				Animator animator = commercial1.transform.GetComponent<Animator>();
               	animator.runtimeAnimatorController = Resources.Load("Models/Building/New_Models/ACs/ACC1") as RuntimeAnimatorController;
			}

			z++;
			x++;
		}
	}

	// Rotate the building to face the road
	void rotateBuilding(GameObject obj, GameObject grid) {
		IList<GameObject> possibleNodes = new List<GameObject> ();

		// Only get nodes that are roads
		possibleNodes = GetWalkableNodes(grid);
		if(possibleNodes.Count > 0) {
			foreach(Transform child in transform) {
				if (child.name.Equals(obj.name + "(Clone)"))
				{
					child.LookAt(possibleNodes[0].transform);
				}
			}
		}
	}

	// Initializes the building objects
	void initializeBuildingObjects() {

		residential1 = Resources.Load("Models/Building/New_Models/residential1") as GameObject; 
		residential2 = Resources.Load("Models/Building/New_Models/residential2") as GameObject; 
		residential3 = Resources.Load("Models/Building/New_Models/residential3") as GameObject; 

		commercial1 = Resources.Load("Models/Building/New_Models/commercial1") as GameObject; 
		commercial2 = Resources.Load("Models/Building/New_Models/commercial2") as GameObject; 
		commercial3 = Resources.Load("Models/Building/New_Models/commercial3") as GameObject; 

		industrial1 = Resources.Load("Models/Building/New_Models/industrial1") as GameObject; 
		industrial2 = Resources.Load("Models/Building/New_Models/industrial2") as GameObject; 
		industrial3 = Resources.Load("Models/Building/New_Models/industrial3") as GameObject; 
	}


	// Instantiates the building object
	void instantiateBuilding (GameObject obj, float x, float y, float z) {

		//Creating each cell of grid
		Instantiate (obj.transform,
			new Vector3 (x,
						 y,
						 z),
						 Quaternion.identity,
						 buildingManager.transform);
	}

	/// <summary>
	/// Gets the walkable nodes.
	/// </summary>
	/// <returns>The walkable nodes.</returns>
	/// <param name="current">Current.</param>
	IList<GameObject> GetWalkableNodes(GameObject current) {
		IList<GameObject> walkableNodes = new List<GameObject> ();
		IList<GameObject> possibleNodes = new List<GameObject> ();
		GameObject up, down, left, right;
		string textMesh = current.transform.GetChild(1).GetComponent<TextMesh> ().text;
		IList<int> points = findNumber (textMesh);
		int x = points [0];
		int z = points [1];

		up = findPlane (x, z + 1);
		down = findPlane (x, z - 1);
		left = findPlane (x - 1, z);
		right = findPlane (x + 1, z);

		possibleNodes.Add (up);
		possibleNodes.Add (down);
		possibleNodes.Add (left);
		possibleNodes.Add (right);

		// Only get nodes that are roads
		foreach (GameObject node in possibleNodes) {
			if(node != null)
			if (node.transform.GetChild (0).GetComponent<TextMesh> ().text == "0")
				walkableNodes.Add (node);

		}

		return walkableNodes;

	}

	/// <summary>
	/// Finds the axis inside plane textmesh.
	/// </summary>
	/// <returns>The number.</returns>
	/// <param name="textMesh">Text mesh.</param>
	public IList<int> findNumber(string textMesh) {
		IList<int> points = new List<int> ();
		char[] delimiterChars = { '{', ',', ' ' , '(', ')' };

		string[] words = textMesh.Split (delimiterChars);
		int temp;

		// Format is (x, z)
		foreach (string s in words) {
			if (int.TryParse (s, out temp))
				points.Add (temp);

		}

		return points;

	}

	/// <summary>
	/// Finds the plane that has certain x and z axis.
	/// </summary>
	/// <returns>The plane.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	GameObject findPlane(int x, int z) {
		string goalPlaneText = "(" + x + ", " + z + ")";
		foreach (GameObject plane in planes) {
			Transform grid = plane.transform;
			string gridText = grid.GetChild (1).GetComponent<TextMesh> ().text;
			if (gridText.Equals (goalPlaneText)) {
				return plane;
			}
		}

		return null;
	}
}