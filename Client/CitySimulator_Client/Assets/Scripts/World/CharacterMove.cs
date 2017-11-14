using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour {

	private int x_src;
	private int z_src;
	private int x_dest;
	private int z_dest;
	private GameObject originalPlane;
//	private bool moving;
//	private int pathIndex;
	private IList<GameObject> path;
	private static float speed = 10f;
	private BFS bfs;

	// For Pathfinding
//	private IDictionary<GameObject, GameObject> nodeParents = new Dictionary<GameObject, GameObject>();

	void Start() {
		bfs = new BFS ();
		x_dest = 24;
		z_dest = 13;

		bfs.OriginalPlane = findCurrentPlane();
		bfs.X_DEST = x_dest;
		bfs.Z_DEST = z_dest;
		bfs.Start ();

	}

	void Update() {
		
		if (bfs.Moving) {
			bfs.Move ();
			float step = Time.deltaTime * speed;
			transform.position = Vector3.MoveTowards (transform.position, bfs.CurrentPlane.transform.position, step);
			if (transform.position.Equals (bfs.CurrentPlane.transform.position) && bfs.PathIndex >= 0) {
				bfs.PathIndex--;
			} 
			Debug.Log ("PATHINDEX: " + bfs.PathIndex);
			if (bfs.PathIndex < 0) {
				bfs.Moving = false;
				bfs.PathIndex = 0;
			}
				

		} else {
			bfs.Move ();
			float step = Time.deltaTime * speed;
			transform.position = Vector3.MoveTowards (transform.position, bfs.CurrentPlane.transform.position, step);
			if (transform.position.Equals (bfs.CurrentPlane.transform.position) && bfs.PathIndex >= 0) {
				bfs.PathIndex++;
			} 
			Debug.Log ("PATHINDEX: " + bfs.PathIndex);
			if (bfs.PathIndex >= bfs.Path.Count) {
				bfs.Moving = true;
				bfs.PathIndex = bfs.Path.Count - 1;
			}
				
			
		}
	}

	GameObject findCurrentPlane() {
		GameObject curr = new GameObject ();
		GameObject[] planes = GameObject.FindGameObjectsWithTag("plane");
		foreach (GameObject plane in planes) {
			if (plane.transform.position.Equals (this.transform.position))
				curr = plane;
		}

		return curr;
	}
//
//	IList<GameObject> getShortestPath(GameObject start, GameObject destination) {
//		IList<GameObject> pathway = new List<GameObject> ();
//		GameObject goal = findShortestPathBFS (start, destination);
//
//		GameObject curr = goal;
//
//		while (!curr.Equals(start)) {
//			pathway.Add (curr);
//			curr = nodeParents [curr];
//		}
//
//		return pathway;
//
//	}
//
//	GameObject findPlane(int x, int z) {
//		string goalPlaneText = "(" + x + ", " + z + ")";
//		foreach (GameObject plane in planes) {
//			Transform grid = plane.transform;
//			string gridText = grid.GetChild (1).GetComponent<TextMesh> ().text;
//			if (gridText.Equals (goalPlaneText)) {
//				return plane;
//			}
//		}
//
//		return null;
//	}
//
//	GameObject findShortestPathBFS(GameObject startPos, GameObject destPos) {
//		Queue<GameObject> queue = new Queue<GameObject> ();
//		HashSet<GameObject> exploredNodes = new HashSet<GameObject> ();
//		queue.Enqueue (startPos);
//
//		while (queue.Count != 0) {
//			GameObject currentNode = queue.Dequeue ();
//			if (currentNode.Equals (destPos)) {
//				return currentNode;
//			}
//
//			IList<GameObject> nodes = GetWalkableNodes (currentNode);
//
//			foreach (GameObject node in nodes) {
//				if (!exploredNodes.Contains (node)) {
//					exploredNodes.Add (node);
//					nodeParents.Add (node, currentNode);
//					queue.Enqueue (node);
//				}
//			}
//
//		}
//
//		return startPos;
//	}
//
//	IList<GameObject> GetWalkableNodes(GameObject current) {
//		string textMesh = current.transform.GetChild(1).GetComponent<TextMesh> ().text;
//		IList<int> points = findNumber (textMesh);
//		int x = points [0];
//		int z = points [1];
//		
//		IList<GameObject> walkableNodes = new List<GameObject> ();
//
//		IList<GameObject> possibleNodes = new List<GameObject> ();
//
//		GameObject up, down, left, right;
//
//		up = findPlane (x, z + 1);
//		down = findPlane (x, z - 1);
//		left = findPlane (x - 1, z);
//		right = findPlane (x + 1, z);
//
//		possibleNodes.Add (up);
//		possibleNodes.Add (down);
//		possibleNodes.Add (left);
//		possibleNodes.Add (right);
//
//		foreach (GameObject node in possibleNodes) {
//			if(node != null)
//				if (node.transform.GetChild (0).GetComponent<TextMesh> ().text == "0")
//					walkableNodes.Add (node);
//
//		}
//
//		return walkableNodes;
//
//	}
//
//	IList<int> findNumber(string textMesh) {
//		IList<int> points = new List<int> ();
//		char[] delimiterChars = { '{', ',', ' ' , '(', ')' };
//
//		string[] words = textMesh.Split (delimiterChars);
//		int temp;
//
//		// Format is (x, z)
//		foreach (string s in words) {
//			if (int.TryParse (s, out temp))
//				points.Add (temp);
//
//		}
//
//		return points;
//
//	}

}
