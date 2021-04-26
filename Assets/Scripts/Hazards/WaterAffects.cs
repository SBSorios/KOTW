using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAffects : MonoBehaviour
{
    public bool wet;
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
            wet = true;
            AudioManager.Instance.PlayClip(soundbyte);
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

        timer -= 1 * Time.deltaTime;
        if(timer <= 0)
        {
            timer = dryTime;
            Dry();
        }
    }

    private void Dry()
    {
        GameManager.Instance.pc.curSpeed = GameManager.Instance.pc.normalSpeed;
        wet = false;
    }
}
