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
            collision.gameObject.GetComponent<Boss>().IncreaseVoid();
        }
        else if (collision.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<EnemyAI>().llave!=null)
            {
                collision.gameObject.GetComponent<EnemyAI>().llave.gameObject.SetActive(true);
            }
            collision.gameObject.GetComponent<EnemyAI>().CancelInvoke();
            collision.gameObject.SetActive(false);
        }
    }
}
