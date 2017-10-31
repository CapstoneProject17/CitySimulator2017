//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//
///**
// * GridCreation that creates 3D based on data from CityDataManager
// * 
// * Author: 		Andrew Lam, Dongwon(Shawn) Kim
// * Date(Cr): 	2017-09-28
// * Date(Mo):	2017-09-28 
// * Reference:	
// * http://answers.unity3d.com/questions/718778/trying-to-create-a-grid.html
// * https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
// * http://answers.unity3d.com/questions/25352/add-texture-to-gameobject-through-code.html
// * https://www.youtube.com/watch?v=810UVUWGlWw
// **/
//public class BuildingCreation : MonoBehaviour {
//
//	public Transform building;
//	public Transform industryBuilding;
//	private GameObject[] planes;
//	private Transform planeTransform;
//
//	// Use this for initialization
//	void Start () {
//		planeTransform = GameObject.Find("Plane(Clone)").transform;
//		CreateBuilding ();
//	}
//
//	// Update is called once per frame
//	void Update () {
//	}
//
//	// Create building model
//	void CreateBuilding () {
//		planes = GameObject.FindGameObjectsWithTag ("plane");
//		int x = 0;
//		int z = 0;
//
//		building.localScale = planeTransform.localScale;
//		building.localScale -= new Vector3 (0.15f, 0, 0.15f);
//
//		industryBuilding.localScale = planeTransform.localScale;
//		industryBuilding.localScale -= new Vector3 (0.15f, 0.30f, 0.30f);
//
//		foreach(GameObject grid in planes) {
//			Transform grid2 = grid.transform;
//			// Residential building models
//			if (grid2.GetChild(0).GetComponent<TextMesh>().text == "1") {
//				Debug.Log ("find 1: " + grid.transform.position.x + ", " + grid.transform.position.y);
//
//				//Creating each cell of grid
//				Instantiate (building,
//					new Vector3 (grid.transform.position.x,
//						0,
//						grid.transform.position.z),
//					Quaternion.identity);
//			}
//
//			//Industrial building objects
//			if (grid2.GetChild(0).GetComponent<TextMesh>().text == "3") {
//				Debug.Log ("find 3: " + grid.transform.position.x + ", " + grid.transform.position.y);
//
//				//Creating each cell of grid
//				Instantiate (industryBuilding,
//					new Vector3 (grid.transform.position.x,
//						grid.transform.position.y,
//						grid.transform.position.z),
//					Quaternion.identity);
//			}
//
//			z++;
//			x++;
//		}
//
//		//		if (plane.GetComponentInChildren<TextMesh> ().text == "1") {
//		//			Instantiate (building);
//		//		}
//	}
//}
