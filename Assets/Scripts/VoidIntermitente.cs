using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidIntermitente : MonoBehaviour
{
    [SerializeField]
    private float tAparicion = 5.0f;
    [SerializeField]
    private bool activo;
    void Start()
    {
        gameObject.SetActive(activo);
        InvokeRepeating("Aparicion", 0f, tAparicion);
    }

    void Update()
    {

    }
    void Aparicion()
    {
        gameObject.SetActive(!activo);
        activo = !activo;

    }
}
