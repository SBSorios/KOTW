using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject unactiveStatue;
    public GameObject activeStatue;
    public Transform resetPOS;

    private void Awake()
    {
        if (!SaveManager.Instance.activeSave.activeCheckpoint)
        {
            unactiveStatue.SetActive(true);
            activeStatue.SetActive(false);
        }
        else
        {
            unactiveStatue.SetActive(false);
            activeStatue.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(!SaveManager.Instance.activeSave.activeCheckpoint)
            {
                GameManager.Instance.mainCamera.GetComponent<CameraFollow>().SwitchTarget(GameManager.Instance.player.transform);
                SaveManager.Instance.activeSave.savedTime = GameManager.Instance.elapsedTime;
                GameManager.Instance.DisplayElapsedTime();
                GameManager.Instance.EndTimer();
                SaveManager.Instance.activeSave.activeCheckpoint = true;
                LevelManager.Instance.SaveToCurLevel();

                unactiveStatue.SetActive(false);
                activeStatue.SetActive(true);
            }
        }
    }
}
