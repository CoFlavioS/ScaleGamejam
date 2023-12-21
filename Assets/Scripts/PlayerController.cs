using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{
    [Range(1, 6)] public int waitToIdle;
    private int sala = 1;
    private Animator anim;
    public bool canWalk;

    Coroutine active;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        active = null;
        canWalk = true;
    }
    void Update()
    {
        Vector2Int moveToDirection = Vector2Int.zero;

        if(canWalk)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                moveToDirection.y = 1;
                if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Arriba"))
                    anim.SetTrigger("up");
                BackToIdle();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                moveToDirection.y = -1;
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Abajo"))
                    anim.SetTrigger("down");
                BackToIdle();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveToDirection.x = 1;
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Derecha"))
                    anim.SetTrigger("right");
                BackToIdle();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveToDirection.x = -1;
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Izquierda"))
                    anim.SetTrigger("left");
                BackToIdle();

            }

            /*if (Input.GetKeyDown(KeyCode.Z) && this.transform.localScale.x < 3)
            {
                ChangeSize(2);
            }
            else if (Input.GetKeyDown(KeyCode.X) && this.transform.localScale.x > 1)
            {
                ChangeSize(-2);
            }*/
        }

        if (moveToDirection != Vector2Int.zero)
        {
            Vector2 newPos = new Vector2(this.transform.position.x, this.transform.position.y);
            newPos += moveToDirection;
            Collider2D col = CanMove(newPos, Mathf.CeilToInt(this.transform.localScale.x));

            if (col == null)
            {
                this.transform.position = newPos;
            }
            else if (!col.CompareTag("Wall"))
            {
                this.transform.position = newPos;
            }
        }
    }

    private void BackToIdle()
    {
        if(active != null)
        {
            StopCoroutine(active);
        }
        active = StartCoroutine(contarTiempoIdle());
    }

    IEnumerator contarTiempoIdle()
    {
        yield return new WaitForSeconds(3f);
        anim.SetTrigger("idle");
    }

    /*void ChangeSize(int sizeVar)
    {
        Collider2D col = null;

        if(sizeVar > 0)
        {
            int i = -1;
            int j = -1;

            while (col == null && i < 2)
            {
                while (col == null && j < 2)
                {
                    if ((i != 0 || j != 0))
                    {
                        col = Physics2D.OverlapPoint(this.transform.position + new Vector3(i, j));
                        Debug.DrawLine(this.transform.position, this.transform.position + new Vector3(i, j), col == null ? Color.green : Color.red, 2);
                    }
                    j++;
                }
                j = -1;
                i++;
            }
        }

        this.transform.localScale += (Vector3.right * sizeVar + Vector3.up * sizeVar);
    }*/

    Collider2D CanMove(Vector2 destination, int scaleVar)
    {
        Collider2D col = null;

        if(scaleVar == 1)
        {
            col = Physics2D.OverlapPoint(destination);
            Debug.DrawLine(this.transform.position, destination, col == null ? Color.green : Color.red, 2);
        }
        else if(scaleVar == 3)
        {
            int i = -1;
            Collider2D aux = null;
            Vector2 check;
            Vector2 moveDir = destination - new Vector2(this.transform.position.x, this.transform.position.y);

            while (i < 2)
            {
                if(moveDir.x == 0)
                {
                    check = destination + Vector2.up * moveDir.y + Vector2.right * i;
                }
                else
                {
                    check = destination + Vector2.up * i + Vector2.right * moveDir.x;
                }
                
                aux = Physics2D.OverlapPoint(check);
                if(aux != null)
                {
                    if (col == null || aux.CompareTag("Wall"))
                    {
                        col = aux;
                    }
                }
                
                if (moveDir.x == 0)
                {
                    Debug.DrawLine(destination + Vector2.right * i, check, col == null ? Color.green : Color.red, 2);
                }
                else
                {
                    Debug.DrawLine(destination + Vector2.up * i, check, col == null ? Color.green : Color.red, 2);
                }
                i++;
            }
        }

        return col;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Checkpoint")
        {
            UnityEngine.Debug.Log("Nuevo checkpoint");
            sala = int.Parse(collider.gameObject.name);
            collider.gameObject.SetActive(false);
        }
        else if (collider.gameObject.tag == "Bullet")
        {
            GetComponent<BoxCollider2D>().enabled = false;
            Die();
        }
    }
    public int GetSala()
    {
        return sala;
    }
    public void Die()
    {
        StartCoroutine(ScaleAnim());
    }
    IEnumerator ScaleAnim()
    {
        canWalk = false;
        while (this.transform.localScale.x>0)
        {
            this.transform.localScale -= (Vector3.right + Vector3.up) * 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        this.transform.localScale = (Vector3.up + Vector3.right) * Mathf.RoundToInt(this.transform.localScale.x);
        GetComponent<BoxCollider2D>().enabled = true;
        GameController.Instance.ReiniciarSala(GameController.Instance.checkpoint);
    }
}
