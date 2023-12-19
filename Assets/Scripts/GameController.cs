using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField]
    private PlayerController player;
    public List<Objeto> sala1;
    public List<Objeto> sala2;
    public List<Objeto> sala3;
    public List<Objeto> sala4;
    public List<Objeto> sala5;
    public List<Objeto> sala6;
    public int checkpoint;
    void Start()
    {
    }
    void Update()
    {
        checkpoint = player.GetSala();
        UnityEngine.Debug.Log(checkpoint);
    }
    public void ReiniciarSala(int checkpoint)
    {
        if (checkpoint == 1)
        {
            for (int i = 0; i < sala1.Count; i++)
            {
                if (!sala1[i].gameObject.activeSelf)
                {
                    sala1[i].gameObject.SetActive(true);
                }
                sala1[i].Reset();
            }
            player.transform.position = new Vector2(-11, 4.5f);
            player.transform.localScale = new Vector3(1,1,1);
        }
        else if (checkpoint == 2)
        {
            for (int i = 0; i < sala2.Count; i++)
            {
                if (!sala2[i].gameObject.activeSelf)
                {
                    sala2[i].gameObject.SetActive(true);
                }
                sala2[i].Reset();
            }
            player.transform.position = new Vector2(-14, -13.5f);
            player.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (checkpoint == 3)
        {
            for (int i = 0; i < sala3.Count; i++)
            {
                if (!sala3[i].gameObject.activeSelf)
                {
                    sala3[i].gameObject.SetActive(true);
                }
                sala3[i].Reset();
            }
            player.transform.position = new Vector2(19, -13.5f);
            player.transform.localScale = new Vector3(3, 3, 1);
        }
        else if (checkpoint == 4)
        {
            for (int i = 0; i < sala4.Count; i++)
            {
                if (!sala4[i].gameObject.activeSelf)
                {
                    sala4[i].gameObject.SetActive(true);
                }
                sala4[i].Reset();
            }
            player.transform.position = new Vector2(53, -26.5f);
            player.transform.localScale = new Vector3(3, 3, 1);
        }
        else if (checkpoint == 5)
        {
            for (int i = 0; i < sala5.Count; i++)
            {
                if (!sala5[i].gameObject.activeSelf)
                {
                    UnityEngine.Debug.Log("a");
                    sala5[i].gameObject.SetActive(true);
                }
                sala5[i].Reset();
            }
            player.transform.position = new Vector2(85,-31.5f);
            player.transform.localScale = new Vector3(3, 3, 1);
        }
        else if (checkpoint == 6)
        {
            for (int i = 0; i < sala6.Count; i++)
            {
                if (!sala6[i].gameObject.activeSelf)
                {
                    sala6[i].gameObject.SetActive(true);
                }
                sala6[i].Reset();
            }
            player.transform.position = new Vector2(56,-51.5f);
            player.transform.localScale = new Vector3(1, 1, 1);
        }     
    }
}
