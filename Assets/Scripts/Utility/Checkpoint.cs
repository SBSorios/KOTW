using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject unactiveStatue;
    public GameObject activeStatue;
    public Transform resetPOS;
    public bool endGate = true;

    /*private void Awake()
    {
        if (!SaveManager.Instance.activeSave.activeCheckpoint && !endGate)
        {
            unactiveStatue.SetActive(true);
            activeStatue.SetActive(false);
        }
        else
        {
            unactiveStatue.SetActive(false);
            activeStatue.SetActive(true);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(!SaveManager.Instance.activeSave.activeCheckpoint)
            {
                if (!endGate)
                {
                    unactiveStatue.SetActive(false);
                    activeStatue.SetActive(true);

                    GameManager.Instance.mainCamera.GetComponent<CameraFollow>().SwitchTarget(GameManager.Instance.player.transform);
                    SaveManager.Instance.activeSave.savedTime = GameManager.Instance.elapsedTime;
                    GameManager.Instance.DisplayElapsedTime();
                    GameManager.Instance.EndTimer();
                    SaveManager.Instance.activeSave.activeCheckpoint = true;
                    LevelManager.Instance.SaveToCurLevel();
                }
            }
        }
    }
}
