using UnityEngine;
using System.Collections;
using System.Linq;

using System;
public enum NodeState{
	Walkable,
	Captured
}
public class Node : MonoBehaviour
{
	public NodeState state;
	public NodeState State{
		get {return state;}
		set{state = value;
			if (state == NodeState.Captured)
				rendering.material.color = ASAO.instance.unWalkableColor;
			else
				rendering.material.color = ASAO.instance.walkableColor;
		}
	}
	public MeshRenderer rendering;
	public Node parent;
    public float gCost
    {
		get{ return Vector3.Distance (ASAO.start.transform.position, transform.position); }
 
    }
    public float hCost
    {
        get { return Vector3.Distance(ASAO.end.transform.position, transform.position); }
    }
    public float fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    void Start()
    {
        rendering = GetComponent<MeshRenderer>();

    }
    public Node[] getNeighbour()
    {
        var nb = ASAO.instance.nodeList.Where(node => condition(node));
        return nb.ToArray();

    }

     bool condition(Node nodeTocheck)
    {
        if ((nodeTocheck.transform.position.x == this.transform.position.x && nodeTocheck.transform.position.y == transform.position.y + Const.neighbourEvalute) ||//top neighbour
            (nodeTocheck.transform.position.x == this.transform.position.x && nodeTocheck.transform.position.y == transform.position.y - Const.neighbourEvalute) || //bottom neighbour
             (nodeTocheck.transform.position.y == this.transform.position.y && nodeTocheck.transform.position.x == transform.position.x + Const.neighbourEvalute) || //right neighbour
             (nodeTocheck.transform.position.y == this.transform.position.y && nodeTocheck.transform.position.x == transform.position.x - Const.neighbourEvalute)|| //left neighbour
			(nodeTocheck.transform.position.y == this.transform.position.y +Const.neighbourEvalute && nodeTocheck.transform.position.x == transform.position.x + Const.neighbourEvalute) ||//left neighbour
			(nodeTocheck.transform.position.y == this.transform.position.y +Const.neighbourEvalute && nodeTocheck.transform.position.x == transform.position.x - Const.neighbourEvalute) ||
			(nodeTocheck.transform.position.y == this.transform.position.y -Const.neighbourEvalute && nodeTocheck.transform.position.x == transform.position.x - Const.neighbourEvalute) ||
			(nodeTocheck.transform.position.y == this.transform.position.y -Const.neighbourEvalute && nodeTocheck.transform.position.x == transform.position.x + Const.neighbourEvalute))
			return true;

        return false;

    }
    private void OnMouseDown()
    {
        ASAO.instance.Push(this);

    }
	private	void OnMouseOver(){
		if (Input.GetMouseButtonDown (1)) {
			State = state == NodeState.Walkable ?  NodeState.Captured : NodeState.Walkable;
		}

	}

}
