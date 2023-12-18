using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] 
    int CHASE_RANGE;

    BoxCollider2D BoxCollider;

    bool Chasing = false;    

    public void Start()
    {
        BoxCollider = GetComponent<BoxCollider2D>();
        BoxCollider.size *= 2 * CHASE_RANGE;
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
        //go to director.playerposition
    }
}




