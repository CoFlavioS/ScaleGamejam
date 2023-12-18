using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{   

    [SerializeField] 
    public static Vector2 PlayerPosition;
    
    [SerializeField]
    public static Vector2 EnemyPosition;

    public bool PlayerInRange(int range)
    {
            return (
            PlayerPosition.x < (EnemyPosition.x + 1) &&
            PlayerPosition.x > (EnemyPosition.x - 1) &&
            PlayerPosition.y < (EnemyPosition.y + 1) &&
            PlayerPosition.y > (EnemyPosition.y - 1));
    }

}
