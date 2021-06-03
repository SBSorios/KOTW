using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public EndGate endGate;
    private bool toggled;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Cursor")
        {
            if (!toggled)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                endGate.opened = false;
                toggled = true;
            }
        }
    }
}
