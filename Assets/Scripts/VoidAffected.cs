using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VoidAffected : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject.transform.localScale.x < 2)
            {
                collision.gameObject.GetComponent<PlayerController>().Die();
            }
        }
        else if (collision.gameObject.name == "Boss")
        {
            Debug.Log("enemigo pisa vacio");
        }
        else if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Entra");
            collision.gameObject.GetComponent<EnemyAI>().CancelInvoke();
            collision.gameObject.SetActive(false);
        }
    }
}
