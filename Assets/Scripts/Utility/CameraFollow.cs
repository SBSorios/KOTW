using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offsets;
    public Vector3 playerOffsets;
    public Vector3 titanOffsets;
    public float transitionSpeed = 20;
    public float cameraPull;
    public float maxTargetDist;
    private float playerDist;

    public Transform target;
    public bool isChanging = false;
    public bool playerCheck;
    public bool titanCheck;

    private void Start()
    {
        if (SaveManager.Instance.hasLoaded)
        {
            if (SaveManager.Instance.activeSave.activeCheckpoint)
            {
                target = GameManager.Instance.player.transform;
                isChanging = true;
            }
        }

        if (GameManager.Instance.playerStart)
        {
            target = GameManager.Instance.player.transform;
            isChanging = true;
        }
    }

    public void SwitchTarget(Transform newTarget)
    {
        isChanging = true;
        target = newTarget;
    }

    private void Update()
    {
        CalculatePlayerDistance();
    }

    private void FixedUpdate()
    {
        CheckIfPlayer();

        if (!GameManager.Instance.playerStart)
        {
            CheckIfTitan();
        }

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

                if (titanCheck)
                {
                    titanCheck = false;
                }


                if (playerCheck)
                {
                    playerCheck = false;
                }
            }
        }

        if (titanCheck && playerDist >= maxTargetDist)
        {
            isChanging = true;
            offsets = new Vector3(titanOffsets.x + cameraPull, titanOffsets.y, -10);
        }
        else if (titanCheck && playerDist <= maxTargetDist)
        {
            isChanging = true;
            offsets = titanOffsets;
        }

        if (playerCheck)
        {
            offsets = playerOffsets;
        }
    }

    public void CheckIfPlayer()
    {
        if(target == GameManager.Instance.player.transform && !playerCheck)
        {
            playerCheck = true;
            titanCheck = false;
        }
    }

    public void CheckIfTitan()
    {
        if(target == GameManager.Instance.killZone.transform && !titanCheck)
        {
            playerCheck = false;
            titanCheck = true;
        }
    }

    private void CalculatePlayerDistance()
    {
        playerDist = Vector2.Distance(target.position, GameManager.Instance.player.transform.position);
    }

    public void ResetFollow()
    {
        Vector3 targetCamPos = new Vector3(target.position.x + offsets.x, transform.position.y, target.position.z + offsets.z);
        transform.position = Vector3.MoveTowards(transform.position, targetCamPos, transitionSpeed * Time.deltaTime);
        if (transform.position == target.position + offsets)
        {
            isChanging = false;

            if (titanCheck)
            {
                titanCheck = false;
            }


            if (playerCheck)
            {
                playerCheck = false;
            }
        }
    }

}
