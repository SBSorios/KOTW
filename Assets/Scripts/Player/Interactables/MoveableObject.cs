using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    public bool attached;
    private GameObject cursor;

    private Vector3 mousePosition;

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
            mousePosition = GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y);
            //this.transform.parent = cursor.transform;
        }
        else
        {
            //this.transform.parent = null;
        }
    }


}
