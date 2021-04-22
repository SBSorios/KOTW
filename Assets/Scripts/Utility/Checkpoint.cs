using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform resetPOS;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Entered Checkpoint!");
            GameManager.Instance.curCheckpoint = resetPOS;
            GameManager.Instance.playerCamera.enabled = true;
            GameManager.Instance.mainCamera.enabled = false;
            //GameManager.Instance.mainCamera.GetComponent<CameraFollow>().target = GameManager.Instance.player.transform;           
        }
    }
}
