using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Collect();   
        }
    }

    public void Collect()
    {
        GameManager.Instance.curCollectibles++;
        LevelManager.Instance.SaveToCurLevel();
        gameObject.SetActive(false);
    }
}
