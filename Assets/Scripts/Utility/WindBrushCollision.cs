using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBrushCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Cursor")
        {
            GameManager.Instance.wc.allowWind = false;
        }
    }

    /*private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //GameManager.Instance.player.transform.position = GameManager.Instance.pc.playerPOS;
            GameManager.Instance.player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5);
            Debug.Log("Should Move Player");
        }
    }*/

    /*private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player Inside Trigger");
            GameManager.Instance.player.transform.position = GameManager.Instance.pc.playerPOS;
        }
    }*/
}
