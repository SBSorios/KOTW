using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform resetPOS;
    private bool triggerCheck;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(!triggerCheck)
            {
                GameManager.Instance.mainCamera.GetComponent<CameraFollow>().SwitchTarget(GameManager.Instance.player.transform);
                SaveManager.Instance.activeSave.activeCheckpoint = true;
                LevelManager.Instance.SaveToCurLevel();
                SaveManager.Instance.Save();

                triggerCheck = true;
            }
        }
    }
}
