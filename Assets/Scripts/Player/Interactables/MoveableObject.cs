using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    public bool attached;
    private GameObject cursor;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Cursor")
        {
            if (GameManager.Instance.pc.allowWind)
            {
                cursor = col.gameObject;
                attached = true;
            }
        }
    }

    private void Update()
    {
        MoveObject();

        if (!GameManager.Instance.pc.allowWind)
        {
            attached = false;
        }
    }

    private void MoveObject()
    {
        if (attached)
        {
            this.transform.parent = cursor.transform;
        }
        else
        {
            this.transform.parent = null;
        }
    }


}
