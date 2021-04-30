using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
    [Header("Non Player Variables")]
    public List<GameObject> ItemsToVacuum = new List<GameObject>();
    public float suctionPower = 1;
    public float pullDistance;
    public bool isVacuuming;
    private Vector3 vacuumDirection;

    [Header("Player Variables")]
    public float vacuumedSpeedAway;
    public float vacuumedSpeedTowards;

    void Update()
    {
        if (isVacuuming)
        {
            VacuumItems();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameManager.Instance.pc.inVacuum = true;
            if (!GameManager.Instance.player.GetComponent<SpriteRenderer>().flipX)
            {
                GameManager.Instance.pc.curSpeed = vacuumedSpeedAway;
            }
            else
            {
                GameManager.Instance.pc.curSpeed = vacuumedSpeedTowards;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameManager.Instance.pc.inVacuum = false;
            GameManager.Instance.pc.curSpeed = GameManager.Instance.pc.normalSpeed;
        }
    }


    void VacuumItems()
    {
        foreach (GameObject pulledObject in ItemsToVacuum)
        {
            if(pulledObject != null)
            {
                vacuumDirection = transform.position - pulledObject.transform.position;
                float distance = Vector2.Distance(transform.position, pulledObject.transform.position);
                Debug.Log(distance);
                if (distance <= pullDistance)
                {
                    pulledObject.GetComponent<Rigidbody2D>().AddForce(vacuumDirection * suctionPower * (1 / Vector3.Distance(transform.position, pulledObject.transform.position)), ForceMode2D.Impulse);
                }
            }
        }
    }
}
