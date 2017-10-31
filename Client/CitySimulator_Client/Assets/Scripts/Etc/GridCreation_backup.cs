//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
///**
// * GridCreation that creates 3D based on data from CityDataManager
// * 
// * Author: 		Dongwon(Shawn) Kim
// * Date(Cr): 	2017-09-18
// * Date(Mo):	2017-09-19 
// * Reference:	
// * http://answers.unity3d.com/questions/718778/trying-to-create-a-grid.html
// * https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
// * http://answers.unity3d.com/questions/25352/add-texture-to-gameobject-through-code.html
// * https://www.youtube.com/watch?v=810UVUWGlWw
// **/
//public class GridCreation : MonoBehaviour {
//
//	public Transform cellPrefab;
//	public Vector3 size;
//	public Random randZone;
////	public int gridWidth;
////	public int gridHeight;
//
//	// Use this for initialization
//	void Start () {
//		CreateGrid ();
//	}
//
//	/**
//	 * Creates grid and put zone, coordinate
//	 **/
//	void CreateGrid(){
//
////		Debug.Log (cellPrefab.localScale.x +", " + cellPrefab.localScale.z);
//
//		for(int x = 0; x < size.x; x++){
//			for(int z = 0; z < size.z; z++){
//
//				//getting random number for zone( its temporally used for prototype)
//				cellPrefab.GetChild (0).GetComponent<TextMesh> ().text = (Random.Range (0, 4)).ToString();
//				cellPrefab.GetChild (1).GetComponent<TextMesh> ().text = "(" + x + ", " + z + ")";
//
//				cellPrefab.tag = "plane";
//
//					
//				//set color index to GridColor to color the grid
//				cellPrefab.GetComponent<GridColor> ().colorIndex = int.Parse(cellPrefab.GetChild (0).GetComponent<TextMesh> ().text);
//
//				//Creating each cell of grid
//			 	Instantiate(cellPrefab, 
//							new Vector3(x + (cellPrefab.localScale.x * x)*10,
//											0,
//											z + (cellPrefab.localScale.z * z)*10),
//							Quaternion.identity);
//				
//			}
//		}
//	}
//
//	void SetCoordinate(){
//	}
//
//	void SetZone() {
//		
//	}
//
//}