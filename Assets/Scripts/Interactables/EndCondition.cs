using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCondition : MonoBehaviour
{
    public string scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(LevelManager.Instance.curLevel == "Level4")
            {
                GameManager.Instance.CalculateScore();
                GameManager.Instance.curLevelComplete = true;
                LevelManager.Instance.SaveToCurLevel();
                LevelManager.Instance.LoadLevel("WinScene");
            }
            else
            {
                GameManager.Instance.CalculateScore();
                UIManager.Instance.levelCompletePanel.SetActive(true);
                GameManager.Instance.curLevelComplete = true;
                GameManager.Instance.pc.canMove = false;
                LevelManager.Instance.SaveToCurLevel();
            }
        }
    }
}
