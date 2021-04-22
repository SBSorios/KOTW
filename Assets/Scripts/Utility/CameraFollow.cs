using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offsets;
    public float transitionSpeed = 20;

    [Tooltip("Assign To The KillZone Object in the TitanEnemy Object")]
    public Transform target;
    private bool isChanging = false;

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
        if(target == GameManager.Instance.player.transform)
        {
            offsets = new Vector3(0, 8, -10);
            Debug.Log("Camera Looking At Player");
        }
    }

}
