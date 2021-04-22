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
            if(GameManager.Instance.curCheckpoint != resetPOS)
            {
                Debug.Log("Entered Checkpoint!");
                GameManager.Instance.curCheckpoint = resetPOS;

                GameManager.Instance.mainCamera.GetComponent<CameraFollow>().SwitchTarget(GameManager.Instance.player.transform);
            }
        }
    }
}
