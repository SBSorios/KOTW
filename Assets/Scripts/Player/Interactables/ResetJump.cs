using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetJump : MonoBehaviour
{

    public bool activated;
    public float activeTime;
    private float timer;
    public SpriteRenderer[] sr;
    public GameObject bottomCollider;
    public GameObject particles;
    public AudioClip cloudPuff;
    public AudioClip cloudEnable;

    private void Start()
    {
        sr = GetComponentsInChildren<SpriteRenderer>();
        timer = activeTime;
    }

    private void Update()
    {
        Activation();
    }

    void Activation()
    {
        if (activated)
        {
            Active();

            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                timer = activeTime;
                activated = false;
            }
        }
        else
        {
            Inactive();
        }
    }

    private void Active()
    {
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color = new Color(1, 1, 1, 1f);
        }
        particles.SetActive(true);
        gameObject.layer = 6;
    }

    private void Inactive()
    {
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color = new Color(1, 1, 1, .5f);
        }
        particles.SetActive(false);
        gameObject.layer = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && activated)
        {
            GameManager.Instance.pc.Jump();
            GameManager.Instance.pc.curAirJump = 0;
        }

        if(collision.gameObject.tag == "Cursor" && !activated)
        {
            GameManager.Instance.wc.allowWind = false;
            AudioManager.Instance.PlayClip(cloudEnable);
            activated = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && activated)
        {
            AudioManager.Instance.PlayClip(cloudPuff);
        }
    }
}
