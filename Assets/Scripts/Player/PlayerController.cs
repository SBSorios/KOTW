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
    public float hangTime = .2f;
    private float hangTimer;
    public float jumpBufferLength;
    public float jumpBufferTimer;
    private int maxAirJump;
    public int curAirJump;
    public bool enableDoubleJump;
    private bool holdingJump;
    public LayerMask groundLayer;

    [Header("Visuals and Audio")]
    public ParticleSystem cloud;
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
        jumpBufferTimer = 0;
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
            hangTimer = hangTime;
        }
        else
        {
            hangTimer -= Time.deltaTime;
        }

        if (stunned)
        {
            StunPlayer();
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferLength = 0.1f;
            jumpBufferTimer = jumpBufferLength;

        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
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
            CloudDust();
        }
        else if (moveInput < 0)
        {
            sr.flipX = true;
            anim.SetBool("Running", true);
            CloudDust();
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
        if(jumpBufferTimer > 0)
        {
            if (hangTimer > 0)
            {
                Jump();
                curAirJump = 1;
                AudioManager.Instance.PlayClip(jump);
            }
            else
            {
                if (curAirJump < maxAirJump)
                {
                    Jump();
                    curAirJump++;
                    AudioManager.Instance.PlayClip(jump);
                }
            }
        }

        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }
    }

    public void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
        CloudDust();
        jumpBufferTimer = 0;
    }

    public void ResetJump()
    {
        Jump();
    }

    private bool IsGrounded()
    {
        float additionalHeight = .35f;
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

    void CloudDust()
    {
        cloud.Play();
    }

}

