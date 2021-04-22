using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offsets;
    public float transitionSpeed = 20;

    public Transform target;
    private bool isChanging = false;
    private bool playerCheck;

    private void Start()
    {
        if (SaveManager.Instance.hasLoaded)
        {
            if (SaveManager.Instance.activeSave.reachedCheckpoint)
            {
                target = GameManager.Instance.player.transform;
            }
        }

        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Titan").transform;
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
            offsets = new Vector3(0, 8, -10);
            playerCheck = true;
        }
    }

}
