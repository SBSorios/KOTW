using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;

    Vector3 offset;

    void Start()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform; 
        }

        offset = transform.position - target.position;
    }

    void Update()
    {
        Follow(target);
    }

    void Follow(Transform obj)
    {
        Vector3 targetCamPos = obj.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
