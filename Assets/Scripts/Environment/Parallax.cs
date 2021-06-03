using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform target;
    private Vector2 startPOS;
    private float startZ;

    private Vector2 travel => (Vector2)Camera.main.transform.position - startPOS;

    private float distFromTarget => transform.position.z - target.position.z;
    private float clippingPlane => (Camera.main.transform.position.z + (distFromTarget > 0 ? Camera.main.farClipPlane : Camera.main.nearClipPlane));
    private float parallaxFactor => Mathf.Abs(distFromTarget) / clippingPlane;

    public void Start()
    {
        target = GameManager.Instance.player.transform;
        startPOS = transform.position;
        startZ = transform.position.z;
    }

    public void FixedUpdate()
    {
        Vector2 newPOS = startPOS + travel * parallaxFactor;
        transform.position = new Vector3(newPOS.x, newPOS.y, startZ);
    }
}
