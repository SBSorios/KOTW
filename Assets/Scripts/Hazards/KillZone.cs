using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [Header("Base Variables")]
    public Transform startPOS;
    public GameObject killZone;
    public GameObject endZone;

    [Header("Chase Variables")]
    public float speed;
    public bool stoppedAtGate;
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

            if (killZone.transform.position.x == endZone.transform.position.x && !stoppedAtGate)
            {
                run = false;
                killZone.SetActive(false);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            GameManager.Instance.BeginTimer();
            GetComponent<BoxCollider2D>().enabled = false;
            run = true;
        }
    }

    public void ResetKillZone()
    {
        killZone.transform.position = startPOS.position;
        Camera.main.GetComponent<CameraFollow>().target = GameManager.Instance.killZone.transform;
    }
}
