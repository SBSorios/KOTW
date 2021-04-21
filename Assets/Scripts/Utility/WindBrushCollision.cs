using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBrushCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Cursor")
        {
            GameManager.Instance.wc.allowWind = false;
        }
    }
}
