using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    public bool attached;
    private GameObject cursor;
    private Vector2 startPosition;
    private Vector3 mousePosition;

    private Rigidbody2D rb;
    public float moveSpeed;
    private void Awake()
    {
        startPosition = gameObject.transform.position;
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bounds")
        {
            Debug.Log(name + " Is Out of Bounds");
            attached = false;
            GameManager.Instance.wc.allowWind = false;
            transform.position = startPosition;
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
            Vector3 direction = (mousePosition - new Vector3(transform.position.x, transform.position.y, 0)).normalized;
            rb.velocity = new Vector2(direction.x * moveSpeed * Time.deltaTime, direction.y * moveSpeed * Time.deltaTime);
            GameManager.Instance.wc.windBrush.GetComponent<CircleCollider2D>().enabled = false;
        }
    }


}
