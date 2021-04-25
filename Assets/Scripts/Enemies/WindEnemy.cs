using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEnemy : MonoBehaviour
{
    
    public float speed; 
    private Transform target;

    void Start()
    {

        // Declare who the target is
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

   
    void Update()
    {
        // Chase the player
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If collides with player, destroy object
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Destroy(this.gameObject);
        }

        // If collides with cursor, destroy object
        if (collision.gameObject.tag == "Cursor")
        {
            GameObject.Destroy(this.gameObject);
        }
    }

}
