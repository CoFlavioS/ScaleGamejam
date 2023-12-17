using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampa : ObjetoActivado
{
    public float cambioX;
    public float cambioY;
    private Transform t;
    void Start()
    {
        t = this.GetComponent<Transform>();
    }
    public override void Activar() {
        Vector3 inicial = t.localScale;
        Vector3 actual= t.localScale;
        StartCoroutine(CambiarEscala(inicial,actual, cambioX, cambioY, 0.1f));
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
}
