using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Probar : MonoBehaviour
{
    public static GameController Instance;
    void Update()
    {
        GameController.Instance.ReiniciarSala(5); 
    }
}