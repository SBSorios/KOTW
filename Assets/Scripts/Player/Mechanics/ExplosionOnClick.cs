using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionOnClick : MonoBehaviour
{

    private Rigidbody2D rb;
  
    void Start()
    {

        rb = this.GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Cursor")
        {
            Debug.Log("Impulse!");

            rb.AddForce(new Vector2(40, 20), ForceMode2D.Impulse);
            
            Object.Destroy(gameObject, 1.0f);          
        
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Collision Exit!");
        Object.Destroy(gameObject, 1.0f);
    }

}
