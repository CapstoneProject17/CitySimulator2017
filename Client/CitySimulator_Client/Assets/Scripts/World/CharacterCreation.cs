using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Module: CharacterCreation
/// Team: Client
/// Description: Creating actors depends on the grid information
/// Author: 
///	 Name: Dongwon(Shawn) Kim   Date: 2017-10-02
/// Modified by:	
///	 Name: N/A   Change: N/A	Date: N/A
/// Based on:  BuildingCreation.cs
public class CharacterCreation : MonoBehaviour {
	// population of the city
	int population;

	// character object
	public Transform character;

	// list of the planes
	private GameObject[] planes;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		population = 10;
		
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		
		CreateCharacter();

	}
	/// <summary>
	/// Creates the character.
	/// </summary>
	void CreateCharacter(){
		planes = GameObject.FindGameObjectsWithTag("plane");

		foreach (GameObject road in planes) {
			if (population <= 0) {
				break;
			}

			if (road.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text == "0") {

				Instantiate(character,
					new Vector3(road.transform.position.x,
						0,
						road.transform.position.z),
					Quaternion.identity);
				population--;
			}

		}

	}
}
