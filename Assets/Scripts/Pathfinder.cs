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
        H = Mathf.Abs(position.x - GoalNode.position.x) + Mathf.Abs(position.y - GoalNode.position.y);
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
            steps.Push(CurrentNode);
            CurrentNode = CurrentNode.parent;
        }
        return steps;
    }

    private void GeneratePath(Node StartNode, Node GoalNode)
    {
        OpenList = new Dictionary<Vector2Int, Node>();
        ClosedList = new Dictionary<Vector2Int, Node>();

        StartNode.F = 0;
        OpenList.Add(StartNode.position, StartNode);

        while (OpenList.Count != 0)
        {
            CurrentNode = SelectBestNode();
            OpenList.Remove(CurrentNode.position);
            Dictionary<Node, int> neighbours = GetNeighbours(CurrentNode);
            foreach (KeyValuePair<Node, int> neighbour in neighbours)
            {
                neighbour.Key.parent = CurrentNode;
                if (neighbour.Key.position == GoalNode.position){
                    return;
                }
                neighbour.Key.G = CurrentNode.G + 10;
                neighbour.Key.CalculateH(GoalNode);
                neighbour.Key.CalculateF();

                if (OpenList.ContainsKey(neighbour.Key.position))
                {
                    if(neighbour.Key.F < OpenList[neighbour.Key.position].F)
                    {
                        OpenList[neighbour.Key.position] = neighbour.Key;
                    }
                }
                else if (!ClosedList.ContainsKey(neighbour.Key.position))
                {
                    OpenList.Add(neighbour.Key.position, neighbour.Key);
                }
            }
            ClosedList[CurrentNode.position] = CurrentNode;
        }
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
