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
public class ScaleEntireObject : MonoBehaviour
{
	public bool doOnce;
	public Vector3 scaleVector;

	void Start(){
		scaleVector = new Vector3(0.1f, 0.1f, 0.1f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (doOnce) {
			GameObject.Find ("BuildingManager").transform.localScale = scaleVector;
			GameObject.Find ("Grid").transform.localScale = scaleVector;
			GameObject.Find ("CharacterManager").transform.localScale = scaleVector;
			doOnce = false;
		}

	}

	/// <summary>
	/// not used yet
	/// </summary>
	void IterateAll(){
		List<GameObject> rootObjects = new List<GameObject> ();
		UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene ();
		scene.GetRootGameObjects (rootObjects);

		// iterate root objects and do something
		for (int i = 0; i < rootObjects.Count; ++i) {
			GameObject gameObject = rootObjects [i];
//			doSomethingToHierarchy (gameObject);

		}
	}

	/// <summary>
	/// not used yet
	/// </summary>
	void Scale(GameObject gObj){
		
		
	}
}

