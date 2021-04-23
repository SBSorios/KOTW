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
            if(GameManager.Instance.curCheckpoint != resetPOS && !triggerCheck)
            {
                Debug.Log("Entered Checkpoint!");
                SaveManager.Instance.activeSave.spawnPosition = resetPOS.transform.position;
                SaveManager.Instance.activeSave.activeCheckpoint = true;
                SaveManager.Instance.Save();

                GameManager.Instance.mainCamera.GetComponent<CameraFollow>().SwitchTarget(GameManager.Instance.player.transform);

                triggerCheck = true;
            }
        }
    }
}
