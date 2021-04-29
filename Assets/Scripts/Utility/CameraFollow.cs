using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offsets;
    public Vector3 playerOffsets;
    public Vector3 titanOffsets;
    public float transitionSpeed = 20;

    public Transform target;
    private bool isChanging = false;
    private bool playerCheck;
    private bool titanCheck;

    private void Start()
    {
        if (SaveManager.Instance.hasLoaded)
        {
            if (SaveManager.Instance.activeSave.activeCheckpoint)
            {
                target = GameManager.Instance.player.transform;
            }
        }
    }

    public void SwitchTarget(Transform newTarget)
    {
        isChanging = true;
        target = newTarget;
    }

    private void FixedUpdate()
    {
        CheckIfPlayer();
        CheckIfTitan();

        if (!isChanging)
        {
            transform.position = target.position + offsets;
        }
        else
        {
            Vector3 targetCamPos = new Vector3(target.position.x + offsets.x, transform.position.y, target.position.z + offsets.z);
            transform.position = Vector3.MoveTowards(transform.position, targetCamPos, transitionSpeed * Time.deltaTime);
            if (transform.position == target.position + offsets)
            {
                isChanging = false;
            }
        }
    }

    public void CheckIfPlayer()
    {
        if(target == GameManager.Instance.player.transform && !playerCheck)
        {
            offsets = playerOffsets;
            playerCheck = true;
            titanCheck = false;
        }
    }

    public void CheckIfTitan()
    {
        if(target == GameManager.Instance.killZone && !titanCheck)
        {
            offsets = titanOffsets;
            playerCheck = false;
            titanCheck = true;
        }
    }

}
