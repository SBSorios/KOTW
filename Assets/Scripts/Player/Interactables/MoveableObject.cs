using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    public bool attached;
    private GameObject cursor;

    private Vector3 mousePosition;
    private Rigidbody2D rb;
    private Vector2 direction;
    public float moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Cursor")
        {
            if (GameManager.Instance.wc.allowWind)
            {
                cursor = col.gameObject;
                attached = true;
            }
        }
    }

    private void Update()
    {
        MoveObject();

        if (!GameManager.Instance.wc.allowWind)
        {
            attached = false;
        }
    }

    private void MoveObject()
    {
        if (attached)
        {
            mousePosition = GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3 (mousePosition.x, mousePosition.y);
        }
        else
        {
            //this.transform.parent = null;
        }
    }


}
