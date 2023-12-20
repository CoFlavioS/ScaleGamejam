using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : MonoBehaviour
{
    public PlayerController player;
    public GameObject proy;
    public float tDisparo;
    public float vel;
    void Start()
    {
        StartCoroutine(Pium());
    }
    void Update()
    {

    }
    IEnumerator Pium()
    {
        while (true)
        {
            yield return new WaitForSeconds(tDisparo);
            PiumAJugador();
        }
    }
    public void PiumAJugador()
    {
        GameObject proyectil = Instantiate(proy, transform.position, Quaternion.identity);
        Rigidbody2D rbP = proyectil.GetComponent<Rigidbody2D>();
        Vector2 dirD = Vector2.left;
        if (player.transform.position.x < 69 && player.transform.position.y > -61.5f)
        {
            dirD = Vector2.left;
        }
        else if (player.transform.position.x >= 69 && player.transform.position.y > -59.5f)
        {
            dirD = Vector2.up;
        }
        else if (player.transform.position.x >= 69 && player.transform.position.y <= -61.5f)
        {
            dirD = Vector2.down;
        }
        else if (player.transform.position.x > 69 && player.transform.position.y > -61.5f)
        {
            dirD = Vector2.right;
        }
        rbP.velocity = dirD * vel;
    }
}
