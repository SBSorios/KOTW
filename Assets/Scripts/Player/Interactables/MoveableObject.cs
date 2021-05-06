using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    public bool attached;
    private GameObject cursor;
    private Vector2 startPosition;
    private Vector3 mousePosition;

    private void Awake()
    {
        startPosition = gameObject.transform.position;
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bounds")
        {
            Debug.Log(name + " Is Out of Bounds");
            attached = false;
            GameManager.Instance.wc.allowWind = false;
            transform.position = startPosition;
        }

        if(col.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), GameManager.Instance.player.GetComponent<Collider2D>());
        }
    }

    private void FixedUpdate()
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
            transform.position = new Vector3(mousePosition.x, mousePosition.y);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GameManager.Instance.wc.windBrush.GetComponent<CircleCollider2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }


}
