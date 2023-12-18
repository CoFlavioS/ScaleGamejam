using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : ObjetoActivado
{
    void Start()
    {
    }
    public override void Activar()
    {
        gameObject.SetActive(false);
    }
    public override void Desactivar()
    {
        gameObject.SetActive(true);
    }
}
