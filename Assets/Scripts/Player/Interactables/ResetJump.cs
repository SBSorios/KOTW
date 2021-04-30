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
        bottomCollider.SetActive(true);
    }

    private void Inactive()
    {
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color = new Color(1, 1, 1, .5f);
        }
        bottomCollider.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && activated)
        {
            Debug.Log("Should Jump On Hit");
            GameManager.Instance.pc.ResetJump();
        }
        else if(collision.gameObject.tag == "Player" && !activated)
        {
            Debug.Log("Should Not Jump On Hit");
        }

        if(collision.gameObject.tag == "Cursor" && !activated)
        {
            GameManager.Instance.wc.allowWind = false;
            activated = true;
            Debug.Log("Cursor");
        }
    }
}
