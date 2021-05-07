using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWindBrush : MonoBehaviour
{
    private Vector3 mousePosition;
    private Rigidbody2D rb;
    private Vector2 direction;

    public float moveSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        mousePosition = GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);

        direction = (mousePosition - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed * Time.deltaTime, direction.y * moveSpeed * Time.deltaTime);
    }
}
