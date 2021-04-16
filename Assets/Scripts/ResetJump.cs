using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetJump : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Should Jump On Hit");
            GameManager.Instance.pc.ResetJump();
        }

        if(collision.gameObject.tag == "Cursor")
        {
            GameManager.Instance.pc.allowWind = false;
            Debug.Log("Cursor");
        }
    }
}
