using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        Vector2Int moveToDirection = Vector2Int.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveToDirection.y = 1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveToDirection.y = -1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveToDirection.x = 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveToDirection.x = -1;
        }

        if (Input.GetKeyDown(KeyCode.Z) && this.transform.localScale.x < 3)
        {
            ChangeSize(2);
        }
        else if (Input.GetKeyDown(KeyCode.X) && this.transform.localScale.x > 1)
        {
            ChangeSize(-2);
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
        }
    }

    void ChangeSize(int sizeVar)
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
                        // Debug.DrawLine(this.transform.position, this.transform.position + new Vector3(i, j), col == null ? Color.green : Color.red, 2);
                    }
                    j++;
                }
                j = -1;
                i++;
            }
        }

        if(col == null)
        {
            this.transform.localScale += (Vector3.right * sizeVar + Vector3.up * sizeVar);
        }
    }

    Collider2D CanMove(Vector2 destination, int scaleVar)
    {
        Collider2D col = null;

        if(scaleVar == 1)
        {
            col = Physics2D.OverlapPoint(destination);
            //Debug.DrawLine(this.transform.position, destination, col == null ? Color.green : Color.red, 2);
        }
        else if(scaleVar == 3)
        {
            int i = -1;
            Vector2 check;
            Vector2 moveDir = destination - new Vector2(this.transform.position.x, this.transform.position.y);

            while (col == null && i < 2)
            {
                if(moveDir.x == 0)
                {
                    check = destination + Vector2.up * moveDir.y + Vector2.right * i;
                }
                else
                {
                    check = destination + Vector2.up * i + Vector2.right * moveDir.x;
                }
                col = Physics2D.OverlapPoint(check);
                /*
                if (moveDir.x == 0)
                {
                    Debug.DrawLine(destination + Vector2.right * i, check, col == null ? Color.green : Color.red, 2);
                }
                else
                {
                    Debug.DrawLine(destination + Vector2.up * i, check, col == null ? Color.green : Color.red, 2);
                }
                */
                i++;
            }
        }

        return col;
    }
}
