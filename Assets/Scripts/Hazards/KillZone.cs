using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public float speed;

    public GameObject killZone;
    public GameObject endZone;

    public bool run;

    private void FixedUpdate()
    {
        if (run)
        {
            killZone.transform.position = Vector2.MoveTowards(killZone.transform.position, endZone.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("Player Triggered KillZone");
            run = true;
        }
    }
}
