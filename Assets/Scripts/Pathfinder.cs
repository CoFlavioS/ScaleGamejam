using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Node
{
    public Vector2Int position;
    public int G,H,F;
    public GameObject tile;
    public bool walkable;
    public Node parent;

    public Node(Vector2Int position)
    {
        this.position = position;
        parent = null;
    }

    public void CalculateF()
    {
        F = G + H;
    }

    public int CalculateH(Node GoalNode)
    {
        H = Mathf.Abs(position.x - GoalNode.position.x) + Mathf.Abs(position.y - GoalNode.position.y); 
        return H;
    }

}

public class Pathfinder : MonoBehaviour
{

    public Tilemap walls;
    Dictionary<Vector2Int, Node> OpenList;
    Dictionary<Vector2Int, Node> ClosedList;
    Node CurrentNode;


    public Stack<Node> GetSteps(Node StartNode, Node GoalNode)
    {   
        Stack<Node> steps = new Stack<Node>();
        Node node = GeneratePath(StartNode, GoalNode);
        //Debug.Log("Current Node Parent" + node.parent.position.x + " " + node.parent.position.y);
        //Debug.Log("Current Node" + node.position.x + " " + node.position.y);
        while (node.parent != null)
        {
            steps.Push(node);
            node = node.parent;
        }
        return steps;
    }

    private Node GeneratePath(Node StartNode, Node GoalNode)
    {   

        Debug.Log("Start Node" + StartNode.position.x + " " + StartNode.position.y);
        Debug.Log("Goal Node" + GoalNode.position.x + " " + GoalNode.position.y);


        OpenList = new Dictionary<Vector2Int, Node>();
        ClosedList = new Dictionary<Vector2Int, Node>();

        StartNode.F = 0;
        OpenList.Add(StartNode.position, StartNode);

        while (OpenList.Count != 0)
        {
            CurrentNode = SelectBestNode();
            Debug.Log("Current Node" + CurrentNode.position.x + " " + CurrentNode.position.y);
            OpenList.Remove(CurrentNode.position);
            Dictionary<Node, int> neighbours = GetNeighbours(CurrentNode);
            foreach (KeyValuePair<Node, int> neighbour in neighbours)
            {
                Debug.Log("Neighbour " + neighbour.Key.position.x + " " + neighbour.Key.position.y);
                neighbour.Key.parent = CurrentNode;
                if (neighbour.Key.position == GoalNode.position){
                    Debug.Log("Goal");
                    CurrentNode = neighbour.Key;
                    return neighbour.Key;
                }
                else 
                {
                    neighbour.Key.G = CurrentNode.G + 10;
                    neighbour.Key.CalculateH(GoalNode);
                    neighbour.Key.CalculateF();
                }

                if (OpenList.ContainsKey(neighbour.Key.position)){
                    if(neighbour.Key.F > OpenList[neighbour.Key.position].F)
                    {
                        Debug.Log("There's a shorter path for this");
                        continue;
                    }
                }
                else if (ClosedList.ContainsKey(neighbour.Key.position)){
                    if(neighbour.Key.F > ClosedList[neighbour.Key.position].F){
                        Debug.Log("I already came here faster in other time");
                        continue;
                    }
                }
                else
                {
                    Debug.Log("I have to visit this one");
                    OpenList.Add(neighbour.Key.position, neighbour.Key);
                }
            }
            Debug.Log("Visited");
            ClosedList.Add(CurrentNode.position, CurrentNode);
        }
        return new Node(new Vector2Int(0,0));
    }

    private Node SelectBestNode()
    {
        Node best = new Node(new Vector2Int(0, 0));
        best.F = 1000000;

        foreach (KeyValuePair<Vector2Int, Node> node in OpenList)
        {
            if (node.Value.F < best.F)
                best = node.Value;
        }
        return best;
    }

    private Dictionary<Node, int> GetNeighbours(Node node)
    {
        Dictionary<Node, int> neighbours = new Dictionary<Node, int>();

        Node topN = new Node(new Vector2Int(node.position.x + 1, node.position.y));
        topN.walkable = !walls.HasTile(new Vector3Int(topN.position.x, topN.position.y, 0));
        if (topN.walkable && !ClosedList.ContainsKey(topN.position))
            neighbours.Add(topN, 0);

        Node bottomN = new Node(new Vector2Int(node.position.x - 1, node.position.y));
        bottomN.walkable = !walls.HasTile(new Vector3Int(bottomN.position.x, bottomN.position.y, 0));
        if (bottomN.walkable && !ClosedList.ContainsKey(bottomN.position))
            neighbours.Add(bottomN, 2);

        Node leftN = new Node(new Vector2Int(node.position.x, node.position.y - 1));
        leftN.walkable = !walls.HasTile(new Vector3Int(leftN.position.x, leftN.position.y, 0));
        if (leftN.walkable && !ClosedList.ContainsKey(leftN.position))
            neighbours.Add(leftN, 1);

        Node rightN = new Node(new Vector2Int(node.position.x, node.position.y + 1));
        rightN.walkable = !walls.HasTile(new Vector3Int(rightN.position.x, rightN.position.y, 0));
        if (rightN.walkable && !ClosedList.ContainsKey(rightN.position))
            neighbours.Add(rightN, 3);

        return neighbours;
    }
}
