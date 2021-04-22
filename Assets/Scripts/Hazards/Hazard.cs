using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public bool instantKill;
    public int damage;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (instantKill)
            {
                GameManager.Instance.ph.Death();
            }
            else
            {
                GameManager.Instance.ph.Damage(damage);
            }
        }
    }
}
