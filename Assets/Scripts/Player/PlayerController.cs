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
    public float stunTime;
    private float moveInput;
    private float timer;
    private Animator anim;
    public bool canMove;
    public bool stunned;
    public bool inVacuum;

    [Header("Jump Variables")]
    public float jumpForce;
    private int maxAirJump;
    private int curAirJump;
    public bool enableDoubleJump;
    private bool holdingJump;
    public LayerMask groundLayer;

    public AudioClip jump;


    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        curSpeed = normalSpeed;
        canMove = true;

        maxAirJump = 1;
        timer = stunTime;
    }

    private void Update()
    {
        if (canMove)
        {
            JumpController();
        }

        if (IsGrounded())
        {
            curAirJump = 0;
        }

        if (stunned)
        {
            StunPlayer();
        }
    }


    void FixedUpdate()
    {
        if (canMove)
        {
            Controller();
        }
    }

    void Controller()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * curSpeed, rb.velocity.y);

        if (moveInput > 0)
        {
            sr.flipX = false;
            anim.SetBool("Running", true);
        }
        else if (moveInput < 0)
        {
            sr.flipX = true;
            anim.SetBool("Running", true);
        }
        else if(moveInput == 0)
        {
            anim.SetBool("Running", false);

            if (inVacuum)
            {
                rb.velocity = new Vector2(-1,0);
            }
        }
    }

    void JumpController()
    {
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
                    if (curAirJump < maxAirJump)
                    {
                        Jump();
                        curAirJump++;
                    }
                }
            }
        }
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;

        //Particle FX here

        if (jump != null)
        {
            AudioManager.Instance.PlayClip(jump);
        }
    }

    public void ResetJump()
    {
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
            anim.SetBool("Grounded", true);
        }
        else
        {
            rayColor = Color.red;
            anim.SetBool("Grounded", false);
        }
        Debug.DrawRay(bc.bounds.center + new Vector3(bc.bounds.extents.x, 0), Vector2.down * (bc.bounds.extents.y + additionalHeight), rayColor);
        Debug.DrawRay(bc.bounds.center - new Vector3(bc.bounds.extents.x, 0), Vector2.down * (bc.bounds.extents.y + additionalHeight), rayColor);
        Debug.DrawRay(bc.bounds.center - new Vector3(bc.bounds.extents.x, bc.bounds.extents.y + additionalHeight), Vector2.right * (bc.bounds.extents.y + additionalHeight), rayColor);
        return raycastHit.collider != null;
    }

    public void StunPlayer()
    {
        canMove = false;
        timer -= Time.deltaTime;
        Debug.Log(timer);
        if(timer <= 0)
        {
            canMove = true;
            stunned = false;
            timer = stunTime;
        }
    }

}

