using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    //public int speed;
    public bool GameStart;

    private void Update()
    {
        if (GameStart)
        {
            transform.Translate(Vector2.right * Time.deltaTime);
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
