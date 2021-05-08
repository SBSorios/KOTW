using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjectGroundCheck : MonoBehaviour
{
    public Rigidbody2D rb;
    private int grounds;
    private bool grounded;
    public MoveableObject mo;
    public float groundFriction;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (grounded && ! mo.attached)
        {
            if (rb.velocity.magnitude < .2f)
            rb.velocity = Vector2.zero;
            else if (rb.velocity.x > 0)
                rb.velocity -= new Vector2(groundFriction * Time.deltaTime, 0);
            else if (rb.velocity.x < 0)
                rb.velocity += new Vector2(groundFriction * Time.deltaTime, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            grounds++;
            if (grounds > 0)
            {
                grounded = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            grounds--;

            if (grounds <= 0)
            {
                grounded = false;
            }
        }
    }
}
