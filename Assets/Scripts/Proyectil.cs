using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
