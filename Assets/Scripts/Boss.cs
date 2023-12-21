using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : Objeto
{
    public PlayerController player;
    public GameObject proy;
    public float tDisparo;
    public float vel;
    private int vacio;
    private bool canShoot;
    public GameObject llaveBoss;
    void Start()
    {
        StartCoroutine(Pium());
        vacio = 0;
        canShoot = true;
    }
    void Update()
    {

    }
    IEnumerator Pium()
    {
        while (true)
        {
            yield return new WaitForSeconds(tDisparo);
            this.gameObject.GetComponent<AudioSource>().Play();
            PiumAJugador();
        }
    }
    public void PiumAJugador()
    {
        if (canShoot)
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
    private void Die()
    {
        StartCoroutine(ScaleAnim());
    }
    public void IncreaseVoid()
    {
        vacio++;
        if (vacio >= 4)
        {
            Die();
        }
    }
    IEnumerator ScaleAnim()
    {
        canShoot = false;
        while (this.transform.localScale.x > 0)
        {
            this.transform.localScale -= (Vector3.right + Vector3.up) * 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        this.transform.localScale = (Vector3.up + Vector3.right) * Mathf.RoundToInt(this.transform.localScale.x);
        llaveBoss.gameObject.SetActive(true);
    }
    public override void Reset()
    {
        vacio = 0;
        canShoot = true;
    }
}
