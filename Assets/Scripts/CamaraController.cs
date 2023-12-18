using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Camera camara;
    void Start()
    {
        
    }
    void Update()
    {
        if (player.transform.position.x > camara.transform.position.x + 17.5f)
        {
            camara.transform.position = new Vector2(camara.transform.position.x + 35, camara.transform.position.y);
        }
        else if (player.transform.position.x < camara.transform.position.x - 17.5f)
        {
            camara.transform.position = new Vector2(camara.transform.position.x - 35, camara.transform.position.y);
        }
        else if (player.transform.position.y > camara.transform.position.y + 10f)
        {
            camara.transform.position = new Vector2(camara.transform.position.x, camara.transform.position.y + 20);
        }
        else if (player.transform.position.y < camara.transform.position.y - 10f)
        {
            camara.transform.position = new Vector2(camara.transform.position.x, camara.transform.position.y - 20);
        }

    }
}
