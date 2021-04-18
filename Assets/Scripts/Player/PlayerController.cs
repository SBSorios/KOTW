using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Instance
    static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<PlayerController>();
            }
            return instance;
        }
    }
    #endregion

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private BoxCollider2D bc;

    [Header("Movement Variables")]
    public float curSpeed;
    public float normalSpeed;
    private float moveInput;

    [Header("Jump Variables")]
    public float jumpForce;
    public bool enableDoubleJump;
    private bool canDoubleJump;
    public LayerMask groundLayer;

    [Header("Mechanic Variables")]
    public GameObject windCursor;
    public bool allowWind = true;
    public float windCooldownTime = 1f;
    private float timer = 0f;
    private Vector3 mousePosition;


    void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc = gameObject.GetComponent<BoxCollider2D>();

        curSpeed = normalSpeed;
    }

    private void Update()
    {
        JumpController();
        IsGrounded();
        WindMechanic();
        WindCooldown();
    }


    void FixedUpdate()
    {
        Controller();
    }

    void Controller()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * curSpeed, rb.velocity.y);

        if (moveInput > 0)
        {
            sr.flipX = false;
        }
        else if (moveInput < 0)
        {
            sr.flipX = true;
        }
    }

    void JumpController()
    {
        if (enableDoubleJump)
        {
            if (IsGrounded())
            {
                canDoubleJump = true;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
            {
                if (IsGrounded())
                {
                    Jump();
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
                    {
                        if (canDoubleJump)
                        {
                            Jump();
                            canDoubleJump = false;
                        }
                    }
                }
            }
        }
        else if (IsGrounded())
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
            {
                rb.velocity = Vector2.up * jumpForce;
            }
        }
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    public void ResetJump()
    {
        canDoubleJump = true;
        Jump();
    }

    private bool IsGrounded()
    {
        float additionalHeight = .5f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, additionalHeight, groundLayer);
        Color rayColor;
        if(raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(bc.bounds.center + new Vector3(bc.bounds.extents.x, 0), Vector2.down * (bc.bounds.extents.y + additionalHeight), rayColor);
        Debug.DrawRay(bc.bounds.center - new Vector3(bc.bounds.extents.x, 0), Vector2.down * (bc.bounds.extents.y + additionalHeight), rayColor);
        Debug.DrawRay(bc.bounds.center - new Vector3(bc.bounds.extents.x, bc.bounds.extents.y + additionalHeight), Vector2.right * (bc.bounds.extents.y + additionalHeight), rayColor);
        return raycastHit.collider != null;
    }

    private void WindMechanic()
    {
        if (Input.GetMouseButton(0) && allowWind)
        {
            WindEnabled();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            allowWind = false;
        }
        else
        {
            WindDisabled();
        }
    }

    public void WindEnabled()
    {
        windCursor.GetComponent<SpriteRenderer>().enabled = true;
        windCursor.GetComponent<CircleCollider2D>().enabled = true;
        windCursor.GetComponent<TrailRenderer>().enabled = true;

        GameManager.Instance.windIconCooldown.enabled = false;
    }

    public void WindDisabled()
    {
        windCursor.GetComponent<SpriteRenderer>().enabled = false;
        windCursor.GetComponent<CircleCollider2D>().enabled = false;
        windCursor.GetComponent<TrailRenderer>().enabled = false;
    }

    public void WindCooldown()
    {
        if (!allowWind)
        {
            timer += 1 * Time.deltaTime;

            GameManager.Instance.windIconCooldown.enabled = true;
            GameManager.Instance.windIconCooldown.fillAmount = timer;

            if (timer >= windCooldownTime)
            {
                timer = 0;
                allowWind = true;
            }
        }
    }
}

