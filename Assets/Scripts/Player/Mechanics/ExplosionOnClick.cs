using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionOnClick : MonoBehaviour
{

    Rigidbody2D rb;

  
    void Start()
    {

        rb = this.GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cursor")
        {
            Debug.Log("Impulse!");

            rb.AddForce(new Vector2(4000, 1000), ForceMode2D.Impulse);
            
            Object.Destroy(gameObject, 1.0f);          

        }

    }

}
