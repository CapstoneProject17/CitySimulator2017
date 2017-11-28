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
///  Name: Lancelei Herradura	Change: character - CharacterMove component  Date: 2017-11-13
///  Name: Lancelei Herradura	Change: set source and destination			 Date: 2017-11-25
///  Name: Lancelei Herradura	Change: random destination and source		 Date: 2017-11-27 
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
		population = 10;
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
		IList<int> xz = new List<int>();
		planes = GameObject.FindGameObjectsWithTag("plane");

		if (population > 0) {

			for (int i = population; i >= 1; i--) {
				GameObject source = setRandSource ();

				Transform human = 
					Instantiate (character,
						new Vector3 (source.transform.position.x, 0, source.transform.position.z),
						Quaternion.identity,characterManager.transform) as Transform;
				
				human.gameObject.AddComponent<CharacterMove> ();
				xz = setRandDest ();
				human.GetComponent<CharacterMove> ().X_Dest = xz[1];
				human.GetComponent<CharacterMove> ().Z_Dest = xz[3];

                Animator animator = human.gameObject.GetComponent<Animator>();
                animator.runtimeAnimatorController = Resources.Load("Models/Character/ACHuman") as RuntimeAnimatorController;
				int x = human.GetComponent<CharacterMove> ().X_Dest;
				int z = human.GetComponent<CharacterMove> ().Z_Dest;
//				Debug.Log("Population i: " + i + "\n" +  x + ", " + z);
				xz.Clear ();
			}

			population = 0;
		}

	}

	/// <summary>
	/// Used for Testing
	/// Sets the rand source.
	/// </summary>
	/// <returns>The rand source.</returns>
	GameObject setRandSource() {
		GameObject source;
		bool isRoad = false;
		System.Random rnd = new System.Random ();

		do {
			source = planes[rnd.Next(planes.Length)].gameObject;
			if (source.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text == "0")
				isRoad = true;

		} while (!isRoad);

		return source;

	}

	/// <summary>
	/// Used for Testing
	/// Sets the rand destination.
	/// </summary>
	/// <returns>The rand destination.</returns>
	IList<int> setRandDest() {
		char[] delimiterChars = { '(', ',', ' ', ')' };
		string gridText;
		string[] words;
		IList<int> xz = new List<int> ();
		GameObject dest;
		bool isRoad = false;
		int check = 0;

		do {
			dest = planes[Random.Range(0, planes.Length-1)].gameObject;
			if (dest.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text == "0") {
				isRoad = true;
				
			}
		} while (!isRoad);


		gridText = dest.transform.GetChild (1).GetComponent<TextMesh> ().text;
		words = gridText.Split (delimiterChars);

		foreach (string s in words)
		{
			int.TryParse (s, out check);
			xz.Add (check);

		}

		return xz;

	}

}
