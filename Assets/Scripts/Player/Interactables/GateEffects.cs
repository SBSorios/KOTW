using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateEffects : MonoBehaviour
{
    public KillZone killZone;
    public AudioClip slamShut;
    public AudioClip fall;

    public void Fall()
    {
        AudioManager.Instance.PlayClip(fall);
    }

    public void Slam()
    {
        AudioManager.Instance.PlayClip(slamShut);
        Camera.main.GetComponent<CameraShake>().StartShake(1.5f, .3f);
    }

    public void StopTitan()
    {
        if(killZone != null)
        {
            if (killZone.stoppedAtGate)
            {
                killZone.run = false;
                GameManager.Instance.killZone.SetActive(false);
                GameManager.Instance.mainCamera.GetComponent<CameraFollow>().SwitchTarget(GameManager.Instance.player.transform);
                SaveManager.Instance.activeSave.savedTime = GameManager.Instance.elapsedTime;
                GameManager.Instance.DisplayElapsedTime();
                GameManager.Instance.EndTimer();
                SaveManager.Instance.activeSave.activeCheckpoint = true;
                LevelManager.Instance.SaveToCurLevel();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "KillZone")
        {
            Debug.Log("Titan Escaped!");
            LevelManager.Instance.LoadLevel("LoseScene");
        }
    }
}
