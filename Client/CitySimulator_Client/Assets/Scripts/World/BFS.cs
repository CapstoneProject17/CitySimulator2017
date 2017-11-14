using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS {

	private int x_dest;
	private int z_dest;
	private IDictionary<GameObject, GameObject> nodeParents = new Dictionary<GameObject, GameObject>();
	private static GameObject[] planes;
	private IList<GameObject> path;
	private GameObject currentPlane;
	private GameObject goal;

	// Use this for initialization
	void Start () {
		planes = GameObject.FindGameObjectsWithTag("plane");
		path = getShortestPath (currentPlane, goal);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int X_DEST {
		get {
			return x_dest;
		}
		set {
			x_dest = value;
		}
	}

	public int Z_DEST {
		get {
			return z_dest;
		}

		set {
			z_dest = value;
		}
	}

	public GameObject CurrentPlane {
		get {
			return currentPlane;
		}
		set {
			currentPlane = value;
		}
	}

	public  GameObject Goal {
		get {
			return goal;
		}

		set {
			goal = value;
		}
	}

	public IList<GameObject> Path {
		get {
			return path;
		}
		set {
			path = value;
		}
	}

	IList<GameObject> getShortestPath(GameObject start, GameObject destination) {
		IList<GameObject> pathway = new List<GameObject> ();
		GameObject goal = findShortestPathBFS (start, destination);

		GameObject curr = goal;

		while (!curr.Equals(start)) {
			pathway.Add (curr);
			curr = nodeParents [curr];
		}

		return pathway;

	}

	GameObject findShortestPathBFS(GameObject startPos, GameObject destPos) {
		Queue<GameObject> queue = new Queue<GameObject> ();
		HashSet<GameObject> exploredNodes = new HashSet<GameObject> ();
		queue.Enqueue (startPos);

		while (queue.Count != 0) {
			GameObject currentNode = queue.Dequeue ();
			if (currentNode.Equals (destPos)) {
				return currentNode;
			}

			IList<GameObject> nodes = GetWalkableNodes (currentNode);

			foreach (GameObject node in nodes) {
				if (!exploredNodes.Contains (node)) {
					exploredNodes.Add (node);
					nodeParents.Add (node, currentNode);
					queue.Enqueue (node);
				}
			}

		}

		return startPos;
	}

	IList<GameObject> GetWalkableNodes(GameObject current) {
		string textMesh = current.transform.GetChild(1).GetComponent<TextMesh> ().text;
		IList<int> points = findNumber (textMesh);
		int x = points [0];
		int z = points [1];

		IList<GameObject> walkableNodes = new List<GameObject> ();

		IList<GameObject> possibleNodes = new List<GameObject> ();

		GameObject up, down, left, right;

		up = findPlane (x, z + 1);
		down = findPlane (x, z - 1);
		left = findPlane (x - 1, z);
		right = findPlane (x + 1, z);

		possibleNodes.Add (up);
		possibleNodes.Add (down);
		possibleNodes.Add (left);
		possibleNodes.Add (right);

		foreach (GameObject node in possibleNodes) {
			if(node != null)
			if (node.transform.GetChild (0).GetComponent<TextMesh> ().text == "0")
				walkableNodes.Add (node);

		}

		return walkableNodes;

	}

	IList<int> findNumber(string textMesh) {
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

	static GameObject findPlane(int x, int z) {
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
