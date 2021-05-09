using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAffects : MonoBehaviour
{
    public bool wet;
    private bool inWater;
    public int slowedSpeed;
    public float dryTime;
    private float timer;
    public AudioClip soundbyte;

    void Start()
    {
        timer = dryTime;
    }

    void Update()
    {
        WaterLogged();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            AudioManager.Instance.PlayClip(soundbyte);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            wet = true;
            inWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            inWater = false;
        }
    }

    private void WaterLogged()
    {
        if (wet)
        {
            Wet();
        }
    }

    private void Wet()
    {
        GameManager.Instance.pc.curSpeed = slowedSpeed;

        if (!inWater)
        {
            timer -= 1 * Time.deltaTime;
            if (timer <= 0)
            {
                timer = dryTime;
                Dry();
            }
        }
    }

    private void Dry()
    {
        GameManager.Instance.pc.curSpeed = GameManager.Instance.pc.normalSpeed;
        wet = false;
    }
}
