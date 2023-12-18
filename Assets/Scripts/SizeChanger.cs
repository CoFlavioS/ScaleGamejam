using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChanger : MonoBehaviour
{
    [SerializeField] bool grow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject.transform.localScale.x == 3 && !grow)
            {
                ChangeSize(collision.transform);
            }
            else if(collision.gameObject.transform.localScale.x == 1 && grow)
            {
                ChangeSize(collision.transform);
            }
        }
    }

    void ChangeSize(Transform player)
    {
        Collider2D col = null;

        if (grow)
        {
            int i = -1;
            int j = -1;

            while (col == null && i < 2)
            {
                while (col == null && j < 2)
                {
                    if ((i != 0 || j != 0))
                    {
                        col = Physics2D.OverlapPoint(player.position + new Vector3(i, j));
                        // Debug.DrawLine(player.position, player.position + new Vector3(i, j), col == null ? Color.green : Color.red, 2);
                    }
                    j++;
                }
                j = -1;
                i++;
            }
        }

        if (col == null)
        {
            StartCoroutine(ScaleAnim(player, (grow ? 1 : -1) * 2));
        }
    }

    IEnumerator ScaleAnim(Transform player, float scaleChange)
    {
        float originalScale = player.localScale.x;
        float a = player.localScale.x;
        float b = (originalScale + scaleChange);

        while (!Mathf.Approximately(player.localScale.x, originalScale + scaleChange))
        {
            player.localScale += (Vector3.right + Vector3.up) * (scaleChange / 10);
            yield return new WaitForSeconds(0.1f);
        }

        player.localScale = (Vector3.up + Vector3.right) * Mathf.RoundToInt(player.localScale.x);
    }
}
