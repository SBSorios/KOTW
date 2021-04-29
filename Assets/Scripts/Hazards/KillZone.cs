using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public float speed;
    public Transform startPOS;
    public GameObject killZone;
    public GameObject endZone;

    public bool run;

    private void Start()
    {
        if (SaveManager.Instance.activeSave.activeCheckpoint)
        {
            killZone.transform.position = endZone.transform.position;
            run = false;
        }
    }

    private void FixedUpdate()
    {
        if (run)
        {
            killZone.transform.position = Vector2.MoveTowards(killZone.transform.position, endZone.transform.position, speed * Time.deltaTime);

            if (killZone.transform.position.x == endZone.transform.position.x)
            {
                run = false;
            }

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

    public void ResetKillZone()
    {
        killZone.transform.position = startPOS.position;
        Camera.main.GetComponent<CameraFollow>().target = GameManager.Instance.killZone.transform;
    }
}
