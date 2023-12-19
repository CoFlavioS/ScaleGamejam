using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] 
    int CHASE_RANGE;

    BoxCollider2D BoxCollider;

    bool Chasing = false;    

    Pathfinder Pathfinder;

    GameObject Player;

    public void Start()
    {
        BoxCollider = GetComponent<BoxCollider2D>();
        BoxCollider.size *= 2 * CHASE_RANGE;
        Pathfinder = GetComponent<Pathfinder>();
    }

    // Update is called once per frame
    void Update()
    {   

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
                BoxCollider.size = new Vector2(4, 4);
                Chase();
            } else {
                //GameOver
            }
        }
    }   


    void Chase()
    {
        Node StartNode = new Node(new Vector2Int((int)transform.position.x, (int)transform.position.y));
        Node GoalNode = new Node(new Vector2Int((int)Player.transform.position.x, (int)Player.transform.position.y));
        Stack<Node> steps = Pathfinder.GetSteps(StartNode, GoalNode);
        Vector2Int nextStep = steps.PoP().position;
        this.transform.position = new Vector3((float)nextStep.x, (float)nextStep.y, 0f);
        sleep(1);
    }
}




