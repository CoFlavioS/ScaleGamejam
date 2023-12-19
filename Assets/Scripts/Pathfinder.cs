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

    public void CalculateH(Node GoalNode)
    {
        H = Mathf.Abs(position.x - GoalNode.position.x) + Mathf(position.y - GoalNode.position.y); 
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
        GeneratePath(StartNode, GoalNode);
        while (CurrentNode.parent != null)
        {
            Stack.Push(CurrentNode);
            CurrentNode = CurrentNode.parent;
        }
        return steps;

    }

    private void GeneratePath(Node StartNode, Node GoalNode)
    {

        OpenList = new Dictionary<Vector2Int, Node>();
        ClosedList = new Dictionary<Vector2Int, Node>();

        StartNode.F = 0;
        OpenList.Add(StartNode);

        while(OpenList.Count != 0)
        {
            CurrentNode = SelectBestNode();
            OpenList.Remove(CurrentNode.position);
            Dictionary<Node, int> neighbours = GetNeighbours(CurrentNode);
            foreach (Node neighbour in neighbours)
                neighbour.parent = CurrentNode;
                if (neighbour.position = GoalNode.position)
                    return;
                neighbour.G = CurrentNode.G + 10;
                neighbour.CalculateH();
                neighbour.CalculateF();
                if (OpenList.Contains(neighbour.position) && neighbour.F > OpenList[neighbour.position].F)
                        continue;
                }
                if (ClosedList.Contains(neighbour.position) && neighbour.F > ClosedList[neighbour.position].F)
                        continue;
                else
                    OpenList.Add(neighbour);
            
            ClosedList.Add(CurrentNode);
            
        }


    }

    private Node SelectBestNode()
    {
        Node best = new Node(new Vector2Int(0, 0));
        best.F = Int.MaxValue;

        foreach (Node node in OpenList)
        {
            if (node.F < best.F)
                best = node;
        }
        return best;
    }

    private Dictionary<Node, int> GetNeighbours(Node node)
    {
        Dictionary<Node, int> neighbours = new Dictionary<Node, int>();

        Node topN = new Node(new Vector2Int(node.position.x + 1, node.position.y));
        topN.walkable = !walls.HasTile(new Vector3Int(topN.x, topN.y, 0));
        if (topN.walkable && !ClosedList.Contains(topN.position))
            neighbours.Add(topN);

        Node bottomN = new Node(new Vector2Int(node.position.x - 1, node.position.y));
        bottomN.walkable = !walls.HasTile(new Vector3Int(bottomN.x, bottomN.y, 0));
        if (bottomN.walkable && !ClosedList.Contains(bottomN.position))
            neighbours.Add(bottomN);

        Node leftN = new Node(new Vector2Int(node.position.x, node.position.y - 1));
        leftN.walkable = !walls.HasTile(new Vector3Int(leftN.x, leftN.y, 0))
        if (leftN.walkable && !ClosedList.Contains(leftN.position))
            neighbours.Add(leftN);

        Node rightN = new Node(new Vector2Int(node.position.x, node.position.y + 1));
        rightN.walkable = !walls.HasTile(new Vector3Int(rightN.x, rightN.y, 0));
        if (rightN.walkable && !ClosedList.Contains(rightN.position))
            neighbours.Add(rightN);
        
        return neighbors;

    }




}
