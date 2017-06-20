using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public  class Const {
     public static readonly int neighbourEvalute = 0x01;
}

public class ASAO : MonoBehaviour
{
    public static ASAO instance;
    public Color walkableColor, unWalkableColor, toggleColor;
    public int gridSize;
   [HideInInspector] public List<Node> nodeList;
    public static Node start, end;

    private void Start()
    {
        instance = this;
        nodeList = new List<Node>();
        Node node = Resources.Load("Node", typeof(Node)) as Node;
		for (int c = 0; c < gridSize; c++)
			for (int r = 0; r < gridSize; r++)
				(Instantiate (node, new Vector2 (c, r), Quaternion.identity) as Node).transform.parent = this.transform;

  
		nodeList = GetComponentsInChildren <Node> ().ToList();
		var minX = nodeList.ToList().First(mNode => mNode.transform.position.x == nodeList.ToList().Min(iNode => iNode.transform.position.x));
		var maxX = nodeList.ToList().First(mNode => mNode.transform.position.x == nodeList.ToList().Max(iNode => iNode.transform.position.x));

		var minY = nodeList.ToList().First(mNode => mNode.transform.position.y == nodeList.ToList().Min(iNode => iNode.transform.position.y));
		var maxY = nodeList.ToList().First(mNode => mNode.transform.position.y == nodeList.ToList().Max(iNode => iNode.transform.position.y));
        Camera.main.transform.position = new Vector3((minX.transform.position.x + maxX.transform.position.x) / 2, (minY.transform.position.y + maxY.transform.position.y) / 2, Camera.main.transform.position.z);
		Camera.main.orthographicSize = gridSize;
	}
   
	public   void Push(Node node)
    {
        if (start == null)
        {
			nodeList.ForEach (n => {
				if (n.state == NodeState.Walkable)
					n.rendering.material.color = Color.white;
			});

            start = node;
            node.rendering.material.color = toggleColor;

        }
        else if (end == null)
        {
            end = node;
            node.rendering.material.color = toggleColor;
			StartCoroutine(PathFinding (start, end));

	

        }

        
    }

	public  IEnumerator PathFinding(Node start, Node end)
    {
			List<Node> openList = new List<Node>();
			List<Node> closedList = new List<Node>();

			Node nStart = nodeList.First(n => n.transform.position.x == start.transform.position.x && n.transform.position.y == start.transform.position.y);
			Node nEnd = nodeList.First(n => n.transform.position.x == end.transform.position.x && n.transform.position.y == end.transform.position.y);
			Debug.Log("nStart " + nStart.transform.position);
			Debug.Log("nEnd " + nEnd.transform.position);
			nStart.rendering.material.color = Color.white;
			nEnd.rendering.material.color = Color.red;
			openList.Add(nStart);

			while (openList.Count != 0)
			{
			
				Node current = openList.FirstOrDefault(node => node.fCost == openList.Min(n => n.fCost));
				openList.Remove(current);
				closedList.Add(current);
				current.rendering.material.color = Color.red;

				if (current == nEnd)
				{
				
					List<Node> path = new List<Node>();
					Node currentNode = end;
				do {
					currentNode.rendering.material.color = Color.blue;
					currentNode.parent.rendering.material.color = Color.yellow;
					path.Add (currentNode);
					currentNode = currentNode.parent;
					yield return new WaitForSeconds (.1f);
					
				} while (currentNode != start);
					path.Add(currentNode);
					currentNode.rendering.material.color = Color.blue;
					path.Reverse();
					Debug.Log ("FOUND PATH");
					ASAO.	start = null;
					ASAO.end = null;
					yield break;
				}

				// foreach
				foreach (Node neighbour in current.getNeighbour())
				{
					//if (neighbour.state == NodeState.Captured || closedList.Contains(neighbour))
					if (neighbour.state == NodeState.Captured && neighbour != nEnd || closedList.Contains(neighbour))
						continue;

					if (!openList.Contains(neighbour))
					{
						openList.Add(neighbour);
						neighbour.parent = current;
						neighbour.rendering.material.color = Color.green;
						yield return new WaitForEndOfFrame ();

					}
				}
			}
		start = null;
		end = null;
		Debug.Log ("NO PATH FOUND");

		}



    




}
