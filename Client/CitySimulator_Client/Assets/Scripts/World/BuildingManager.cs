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

	private GameObject buildingObject;

	private Transform planeTransform;
	// private IList<GameObject> testplanes = new List<GameObject>();
	// private IList<GameObject> testbuildings = new List<GameObject>();

	private GameObject residential1;
	private GameObject residential2;
	private GameObject residential3;
	private GameObject commercial1;
	private GameObject commercial2;
	private GameObject commercial3;
	private GameObject industrial1;
	private GameObject industrial2;
	private GameObject industrial3;

	// Use this for initialization
	void Start () {
		// planeTransform = GameObject.Find("Plane(Clone)").transform;
		buildingManager = GameObject.Find ("BuildingManager");
		initializeBuildingObjects();
		intializeBuildings ();
	}

	// Update is called once per frame
	void Update () {
		intializeBuildings ();
	}

	// Update builings
	void updateBuildings (string id) {

		// Find the building object with the matching id
		GameObject building = GameObject.Find( id + "(Clone)");
	}

	// Create new building to on given grid location
	public void createBuilding (string GUID, int x, int z, short type, int rating) {

		// Finds the type of the building. eg. Industrial, Residential, Commercial.
		switch(type) 
		{
			// Residental 
			case 1: 
				if(rating == 1) {
				residential1.name = GUID;
				instantiateBuilding (residential1,
									 x,
									 0,
									 z);
				}

				if(rating == 2) {
				residential2.name = GUID;
				instantiateBuilding (residential2,
									 x,
									 0,
									 z);
				}

				if(rating == 3) {				
				residential3.name = GUID;
				instantiateBuilding (residential3,
									 x,
									 0,
									 z);
				}
			break; 

			// Commercial
			case 2:
				if(rating == 1) {
				commercial1.name = GUID;
				instantiateBuilding (commercial1,
									 x,
									 0,
									 z);
				}

				if(rating == 2) {
				commercial2.name = GUID;
				instantiateBuilding (commercial2,
									 x,
									 0,
									 z);
				}

				if(rating == 3) {				
				commercial3.name = GUID;
				instantiateBuilding (commercial3,
									 x,
									 0,
									 z);
				}
			break;

			// Industrial
			case 3:
				if(rating == 1) {				
				industrial1.name = GUID;
				instantiateBuilding (industrial1,
									 x,
									 0,
									 z);
				}

				if(rating == 2) {
				industrial2.name = GUID;
				instantiateBuilding (industrial2,
									 x,
									 0,
									 z);
				}

				if(rating == 3) {
				industrial3.name = GUID;
				instantiateBuilding (industrial3,
									 x,
									 0,
									 z);
				}
			break;
		}
	}

	// Dispose building 
	public void disposeBuilding (string id) {

		GameObject building = GameObject.Find( id + "(Clone)");
		
		// Animation for destroying building
		//
		// while (building.transform.localScale.y > 0.0) {
		// 	building.transform.localScale -= new Vector3(building.transform.localScale.x * 0.5, 
		// 												building.transform.localScale.y * 0.5, 
		// 												building.transform.localScale.z * 0.5);
		// }

		Destroy(building);
	}

	// initialize building models
	public void intializeBuildings () {
		planes = GameObject.FindGameObjectsWithTag ("plane");
		int x = 0;
		int z = 0;

		// building.localScale = planeTransform.localScale;
		// building.localScale -= new Vector3 (0.25f, 0.20f, 0.15f);

		// industryBuilding.localScale = planeTransform.localScale;
		// industryBuilding.localScale -= new Vector3 (0.15f, 0.35f, 0.30f);

		foreach(GameObject grid in planes) {

			// Residential building models
			if (grid.transform.GetChild(0).GetComponent<TextMesh>().text == "1") {
//				Debug.Log ("find 1: " + grid.transform.position.x + ", " + grid.transform.position.y);
				
				//Creating each cell of grid
				instantiateBuilding(residential2, 
									grid.transform.position.x,
									grid.transform.position.y,
									grid.transform.position.z);
				
				// rotateBuilding(residential2, grid);
			}

			//Commercial building objects
			if (grid.transform.GetChild(0).GetComponent<TextMesh>().text == "3") {
//				Debug.Log ("find 3: " + grid.transform.position.x + ", " + grid.transform.position.y);
								
				instantiateBuilding(commercial1, 
									grid.transform.position.x,
									grid.transform.position.y,
									grid.transform.position.z);
			}

			z++;
			x++;
		}
	}

	// Rotate the building to face the road
	void rotateBuilding(GameObject obj, GameObject grid) {
		IList<GameObject> possibleNodes = new List<GameObject> ();

		possibleNodes = GetWalkableNodes(grid);

		//Debug.Log(possibleNodes.Count);
		//Debug.Log(possibleNodes[0].transform.GetChild(1).GetComponent<TextMesh>().text);
		
		if(possibleNodes.Count != 0) {
			// float smooth = 2.0F;
	    	// float tiltAngle = 90.0F;
	     //    float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
	     //    float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
	     //    Quaternion target = Quaternion.Euler(tiltAroundX, 90, tiltAroundZ);
	        // transform.localRotation = Quaternion.Euler(tiltAroundX, 90, tiltAroundZ);
	        // transform.localRotation(Vector3.up * 90, Space.Self);
	        // transform.Translate(new Vector3(transform.position.z, transform.position.y, transform.position.x));
	        transform.localRotation = Quaternion.Euler(new Vector3(0,90,0));

		}
	}

	// Initializes the building objects
	void initializeBuildingObjects() {

		residential1 = Resources.Load("Models/Building/Completed_Models/residential1") as GameObject; 
		residential2 = Resources.Load("Models/Building/Completed_Models/residential2") as GameObject; 
		residential3 = Resources.Load("Models/Building/Completed_Models/residential3") as GameObject; 

		commercial1 = Resources.Load("Models/Building/Completed_Models/commercial1") as GameObject; 
		commercial2 = Resources.Load("Models/Building/Completed_Models/commercial2") as GameObject; 
		commercial3 = Resources.Load("Models/Building/Completed_Models/commercial3") as GameObject; 

		industrial1 = Resources.Load("Models/Building/Completed_Models/industrial1") as GameObject; 
		industrial2 = Resources.Load("Models/Building/Completed_Models/industrial2") as GameObject; 
		industrial3 = Resources.Load("Models/Building/Completed_Models/industrial3") as GameObject; 
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