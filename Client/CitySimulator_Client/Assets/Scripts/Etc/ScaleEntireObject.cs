using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Module: Scale entire object.
/// Team: Client
/// Description: creates 3D grid based on data from CityDataManager
/// Author: 
///	 Name: Dongwon(Shawn) Kim    Date: 2017-10-31
/// Modified by:	
/// Based on:  
/// http://answers.unity3d.com/questions/7615/how-do-i-iterate-over-all-scene-objects-from-an-ed.html
/// http://answers.unity3d.com/questions/881522/scaling-down-every-object-in-a-scene-best-practice.html
/// </summary>
public class ScaleEntireObject : MonoBehaviour {

	// bool value for apply only once
	public bool doOnce;
	// scale vector: how much the objects will be scaled
	public Vector3 scaleVector;

	/// <summary>
	/// Starts this instance
	/// </summary>
	void Start(){
		scaleVector = new Vector3(0.1f, 0.1f, 0.1f);
	}
	
	
	/// <summary>
	/// Update this instance
	/// </summary>
	void Update () {

		// checks doOnce value is true
		if (doOnce) {

			// and apply all objects in the city to be scaled
			GameObject.Find ("BuildingManager").transform.localScale = scaleVector;
			GameObject.Find ("Grid").transform.localScale = scaleVector;
			GameObject.Find ("CharacterManager").transform.localScale = scaleVector;
			
			//turned off
			doOnce = false;
		}

	}

	/// <summary>
	/// Iterates all the gameobjects form the current scene
	/// *not used yet
	/// </summary>
	void IterateAll(){

		//List of the objects
		List<GameObject> rootObjects = new List<GameObject> ();

		// get current scene reference
		UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene ();
		
		// get a root object from the scene
		scene.GetRootGameObjects (rootObjects);

		// iterate root objects and do something
		for (int i = 0; i < rootObjects.Count; ++i) {
			
//			doSomethingToHierarchy (gameObject);

		}
	}

	/// <summary>
	/// not used yet
	/// </summary>
	void Scale(GameObject gObj){
		
		
	}
}

