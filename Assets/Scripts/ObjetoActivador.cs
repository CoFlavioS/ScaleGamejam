using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjetoActivador : MonoBehaviour
{
    [SerializeField]
    private ObjetoActivado oA;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        oA.Activar();
        if (gameObject.tag == "Key")
        {
            gameObject.SetActive(false);
        }
        else if (gameObject.tag == "Button")
        {
            //Cambiar al sprite del votón pulsado
        }
    }
}
