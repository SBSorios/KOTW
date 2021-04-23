using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.isKinematic = false;
            rb.AddForce(new Vector2(0, -5), ForceMode2D.Impulse);
        }
    }
}
