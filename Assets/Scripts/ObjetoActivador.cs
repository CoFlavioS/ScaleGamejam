using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjetoActivador : Objeto
{
    public ObjetoActivado oA;
    public bool activoAlPrincipio;
    void Start()
    {

    }
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D c)
    {
        this.gameObject.GetComponent<AudioSource>().Play();
        oA.Activar();
        StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(1.5f);
        if (gameObject.tag == "Key")
        {
            gameObject.SetActive(false);
        }
        else if (gameObject.tag == "Button")
        {
            //Cambiar al sprite del botón pulsado
        }
    }

    public override void Reset()
    {
        if (gameObject.tag == "Key")
        {
            if (activoAlPrincipio == false)
            {
                gameObject.SetActive(false);
            }
        }
        else if (gameObject.tag == "Button")
        {
            //Cambiar al sprite del botón pulsado
        }
    }
}
