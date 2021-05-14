using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricEnemy : MonoBehaviour
{
    public float speed;
    public AudioClip hitPlayer;
    public AudioClip hitEnemy;
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.pc.stunned = true;
            GameObject.Destroy(this.gameObject);
            AudioManager.Instance.PlayClip(hitPlayer);

        }

        if (collision.gameObject.tag == "Cursor")
        {
            AudioManager.Instance.PlayClip(hitEnemy);
            GameObject.Destroy(this.gameObject);
        }
    }
}
