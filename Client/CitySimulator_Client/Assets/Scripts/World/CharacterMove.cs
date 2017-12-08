using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Module: CharacterMove
/// Team: Client
/// Description: Make human move to location and back
/// Author: 
///	 Name: Lancelei Herradura   Date: 2017-11-13
/// Modified by:
///  Name: Lancelei Herradura	Change:	All calls happen in update	Date: 2017-11-25
///  Name: Lancelei Herradura	Change: Got rid of new object bug	Date: 2017-11-27
///  Name: Lancelei Herradura	Change: Only goes to the goal		Date: 2017-11-28
/// Based on:  How to Do PATHFINDING: The Basics (Graphs, BFS, and DFS in Unity) 
/// https://www.youtube.com/watch?v=WvR9voi0y2I
/// GitHub Username: anneomcl
/// https://github.com/anneomcl/PathfindingBasics
/// </summary>
public class CharacterMove : MonoBehaviour {

	public bool StartMove = false;
	public bool StartBFS = false;
	// X axis of destination
	private static int x_dest;
	/// <summary>
	/// Gets or sets the x destination.
	/// </summary>
	/// <value>The x destination.</value>
	public int X_Dest {
		get {
			return x_dest;
		}
		set {
			x_dest = value;
			bfs.X_DEST = x_dest;
		}
	}
	// Z axis of destination
	private static int z_dest;
	/// <summary>
	/// Gets or sets the z destination.
	/// </summary>
	/// <value>The z destination.</value>
	public int Z_Dest {
		get {
			return z_dest;
		}
		set {
			z_dest = value;
			bfs.Z_DEST = z_dest;
		}
	}

	private static int x_src;
	public int X_Src {
		get {
			return x_src;
		}
		set {
			x_src = value;
		}
	}

	private static int z_src;
	public int Z_Src {
		get {
			return z_src;
		}
		set {
			z_src = value;
		}
	}

	// Plane on which character is on
	public GameObject originalPlane;
	// Movement speed
	private static float speed = 10f;
	// access all BFS functions
	private BFS bfs = new BFS();

	private Dictionary<int, Dictionary<int,GameObject>> grid;
	public Dictionary<int, Dictionary<int,GameObject>> Grid {
		get {
			return grid;
		}
		set {
			grid = value;
			bfs.Grid = grid;
		}
	}


	/// <summary>
	/// Start this instance.
	/// For now, retrieve information at Start.
	/// </summary>
	void Start() {

	}


	/// <summary>
	/// Update this instance.
	/// Character moves to destination one plane at a time.
	/// Move to the location back and forth.
	/// </summary>
	void Update() {
		if(StartBFS){
			bfs.OriginalPlane = originalPlane;
			bfs.Start ();
			StartBFS = false;
			StartMove = true;
		}

		if(StartMove){
			move ();
		}

	}

	/// <summary>
	/// Move this instance.
	/// </summary>
	void move() {
		Vector3 targetDir;
		Vector3 newDir;
		float step = Time.deltaTime * speed;

		if (bfs.Valid) {
			bfs.Move ();
			targetDir = bfs.CurrentPlane.transform.position - transform.position;
			newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
			transform.position = Vector3.MoveTowards (transform.position, bfs.CurrentPlane.transform.position, step);
			transform.rotation = Quaternion.LookRotation (newDir);
			if (bfs.To) {

				if (transform.position.Equals (bfs.CurrentPlane.transform.position) && bfs.PathIndex >= 0) {
					bfs.PathIndex--;
				} 
				if (bfs.PathIndex < 0) {
					bfs.To = false;
					bfs.PathIndex = 0;
				}

			} else {
				Destroy(this.gameObject);
			}

		}
	}


}