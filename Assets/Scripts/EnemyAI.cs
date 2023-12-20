using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Vector3 lastPosition;
    [Range(1, 6)] public int waitToIdle;

    [SerializeField] 
    //Vector2Int CHASE_RANGE;
    [Range(0.1f, 1)]
    public float repeatRate;

    BoxCollider2D BoxCollider;

    bool Chasing;    

    Pathfinder Pathfinder;

    [SerializeField]
    GameObject Player;

    private Animator anim;
    Coroutine active;

    public void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        BoxCollider = GetComponent<BoxCollider2D>();
        //BoxCollider.size = CHASE_RANGE;
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
                BoxCollider.size = new Vector2(0.9f,0.9f);
                BoxCollider.offset = new Vector2(0, 0);
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

            Vector3 direction = (transform.position - lastPosition).normalized;
            lastPosition = transform.position;

            if (direction.x > 0)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("BugDer"))
                    anim.SetTrigger("right");
                BackToIdle();
            }
            else if (direction.x < 0)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("BugIzq"))
                    anim.SetTrigger("left");
                BackToIdle();
            }
            else if (direction.y > 0)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("BugUp"))
                    anim.SetTrigger("up");
                BackToIdle();
            }
            else if (direction.y < 0)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("BugDown"))
                    anim.SetTrigger("down");
                BackToIdle();
            }

        }
        else
        {
            //GameOver
        }
    }

    private void BackToIdle()
    {
        if (active != null)
        {
            StopCoroutine(active);
            //Debug.Log("reset");
        }
        active = StartCoroutine(contarTiempoIdle());
    }

    IEnumerator contarTiempoIdle()
    {
        yield return new WaitForSeconds(3f);
        anim.SetTrigger("idle");
        //Debug.Log("Trigger");
    }
}




