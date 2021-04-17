using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public float speed;

    private GameObject startZone;
    public GameObject endZone;

    public bool Run;

    private void Start()
    {
        startZone = gameObject;
    }

    private void Update()
    {
        if (Run)
        {
            //transform.Translate(Vector2.right * Time.deltaTime);
            transform.position = Vector2.MoveTowards(startZone.transform.position, endZone.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered Killzone");
            GameManager.Instance.ph.Damage(GameManager.Instance.ph.totalHealth);
        }
    }
}
