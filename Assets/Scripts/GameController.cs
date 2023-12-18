using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    public List<GameObject> sala1;
    public List<GameObject> sala2;
    public List<GameObject> sala3;
    public List<GameObject> sala4;
    public List<GameObject> sala5;
    public List<GameObject> sala6;
    public int checkpoint;
    void Start()
    {
    }
    public void ReiniciarSala(int checkpoint)
    {
        if (checkpoint == 1)
        {
            for (int i = 0; i < sala1.Count; i++)
            {
                sala1[i].SetActive(true);
            }
        }
        else if (checkpoint == 2)
        {
            for (int i = 0; i < sala2.Count; i++)
            {
                sala2[i].SetActive(true);
            }
        }
        else if (checkpoint == 3)
        {
            for (int i = 0; i < sala3.Count; i++)
            {
                sala3[i].SetActive(true);
            }
        }
        else if (checkpoint == 4)
        {
            for (int i = 0; i < sala4.Count; i++)
            {
                sala4[i].SetActive(true);
            }
        }
        else if (checkpoint == 5)
        {
            for (int i = 0; i < sala5.Count; i++)
            {
                sala5[i].SetActive(true);
            }
        }
        else if (checkpoint == 6)
        {
            for (int i = 0; i < sala6.Count; i++)
            {
                sala6[i].SetActive(true);
            }
        }     
    }
}
