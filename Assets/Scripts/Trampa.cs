using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampa : ObjetoActivado
{
    public float cambioX;
    public float cambioY;
    private Transform t;
    private bool activada;
    private float xInicial;
    private float yInicial;
    void Start()
    {
        t = this.GetComponent<Transform>();
        activada = false;
        xInicial = t.localScale.x;
        yInicial = t.localScale.y;
    }
    public override void Activar() {
        if (!activada)
        {
            Vector3 inicial = t.localScale;
            Vector3 actual = t.localScale;
            StartCoroutine(CambiarEscala(inicial, actual, cambioX, cambioY, 0.1f));
        }
        activada = true;
    }
    private IEnumerator CambiarEscala(Vector3 inicial,Vector3 actual,float cambioX, float cambioY, float duracion)
    {
        float tiempo = 0f;
        while (tiempo < duracion)
        {
            actual.x= inicial.x+(cambioX * tiempo / duracion);
            actual.y = inicial.y+(cambioY * tiempo / duracion);
            t.localScale = actual;
            yield return new WaitForSeconds(0.01f);
            tiempo += 0.01f;
        }
        t.localScale = new Vector3(inicial.x+cambioX,inicial.y+cambioY,1f);
    }
    public override void Desactivar()
    {
        activada= false;
        t.localScale = new Vector3(xInicial,yInicial,1f);
    }
}
