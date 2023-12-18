using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] 
    int CHASE_RANGE;

    bool Chasing = false;

    public enum EnemyState 
    {
        WANDER,
        CHASE
    }

    // Update is called once per frame
    void Update()
    {   
        if (!Chasing)
        {
            if (Director.PlayerInRange(CHASE_RANGE))
                Chasing = true;
        } else {
            Chase();
        }

        if (Director.PlayerInRange(1))
        {
            //GameOver
        }
        UpdateDirectorValues();
    }

    void UpdateDirectorValues()
    {
        Director.EnemyPosition.Set(this.transform.position.x, this.transform.position.y);
    }

    void Chase()
    {
        //go to director.playerposition
    }
}




