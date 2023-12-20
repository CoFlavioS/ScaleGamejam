using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] 
    Vector2Int CHASE_RANGE;
    [Range(0.1f, 1)]
    public float repeatRate;

    BoxCollider2D BoxCollider;

    bool Chasing;    

    Pathfinder Pathfinder;

    [SerializeField]
    GameObject Player;

    public void Start()
    {
        BoxCollider = GetComponent<BoxCollider2D>();
        BoxCollider.size = CHASE_RANGE;
        Pathfinder = GetComponent<Pathfinder>();
        Chasing = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter");
        if (other.tag == "Player")
        {
            Debug.Log("Player Detected!!");
            if (!Chasing)
            {
                Chasing = true;
                BoxCollider.size = new Vector2(1, 1);
                InvokeRepeating("Chase", 0f, repeatRate);
            } else {
                //GameOver
            }
        }
    }


    void Chase()
    {
        Debug.Log("chaseStart");
        Node StartNode = new Node(new Vector2Int((int)transform.position.x, (int)transform.position.y));
        Node GoalNode = new Node(new Vector2Int((int)Player.transform.position.x, (int)Player.transform.position.y));
        Collider2D col = Physics2D.OverlapPoint(Player.transform.position + Vector3.right);
        if (Vector2.Distance(StartNode.position, GoalNode.position) >= Mathf.Sqrt(2))
        {
        Stack<Node> steps = Pathfinder.GetSteps(StartNode, GoalNode);
        Vector2Int nextStep = steps.Pop().position;
        this.transform.position = new Vector3((float)nextStep.x, (float)nextStep.y, 0f);
        }
        else
        {
            Debug.Log("A tu casa");
        }
    }
}




