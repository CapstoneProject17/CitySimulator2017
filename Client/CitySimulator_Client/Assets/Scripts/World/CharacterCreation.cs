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
///	 Name: Dongwon(Shawn) Kim   Change: initiate belongs to CharacterManager Date: 2017-10-31
/// Based on:  BuildingCreation.cs
public class CharacterCreation : MonoBehaviour {
	// population of the city
	int population;

	// character object
	public Transform character;

	// list of the planes
	private GameObject[] planes;

	// The character manager.
	private GameObject characterManager;


	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		population = 2;
		characterManager = GameObject.Find ("CharacterManager");
		
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		
		createCharacter();

	}

	/// <summary>
	/// Creates the character.
	/// </summary>
	void createCharacter(){
		planes = GameObject.FindGameObjectsWithTag("plane");

		foreach (GameObject road in planes) {
			if (population <= 0) {
				break;
			}

			if (road.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text == "0") {
				Transform human = 
					Instantiate (character,
						new Vector3 (road.transform.position.x, 0, road.transform.position.z),
						Quaternion.identity,characterManager.transform) as Transform;
				population--;
				human.gameObject.AddComponent<CharacterMove> ();
				List<int> xz = setRandDest ();

				// Set the destination
				human.GetComponent<CharacterMove> ().X_Dest = xz[1];
				human.GetComponent<CharacterMove> ().Z_Dest = xz[3];

			}

		}

	}

	// Testing: Set Random Destination
	List<int> setRandDest() {
		char[] delimiterChars = { '(', ',', ' ', ')' };
		List<int> xz = new List<int> ();
		GameObject dest;
		bool isRoad = false;
		System.Random rnd = new System.Random ();
		int check = 0;

		do {
			dest = planes[rnd.Next(planes.Length)].gameObject;
			if (dest.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text == "0") {
				isRoad = true;
				
			}
		} while (!isRoad);


		string gridText = dest.transform.GetChild (1).GetComponent<TextMesh> ().text;
		string[] words = gridText.Split (delimiterChars);

		foreach (string s in words)
		{
			int.TryParse (s, out check);
			xz.Add (check);

		}

		return xz;

	}

}
