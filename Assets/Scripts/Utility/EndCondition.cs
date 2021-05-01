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
            GameManager.Instance.CalculateScore();
            UIManager.Instance.levelCompletePanel.SetActive(true);
            GameManager.Instance.curLevelComplete = true;
            GameManager.Instance.pc.canMove = false;
            LevelManager.Instance.SaveToCurLevel();
        }
    }
}
