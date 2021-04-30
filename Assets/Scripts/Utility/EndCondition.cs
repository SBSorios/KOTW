using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCondition : MonoBehaviour
{
    public string scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.curLevelComplete = true;
            LevelManager.Instance.SaveToCurLevel();
            LevelManager.Instance.LoadLevel("LevelSelect");
        }
    }
}
