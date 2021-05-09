using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CameraFollow cf;
    private float transformY;
    private float shakeTimeRemaining;
    private float shakePower;
    private float shakeFadeTime;
    private float shakeRotation;
    public float rotationMulitplier = 5;
    private bool reset;

    private void Awake()
    {
        cf = GetComponent<CameraFollow>();
        transformY = transform.position.y;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartShake(.2f, .5f);
        }
    }

    private void LateUpdate()
    {
        if(shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-.5f, .5f) * shakePower;
            float yAmount = Random.Range(-.5f, .5f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0);
            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);

            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMulitplier * Time.deltaTime);
        }
        else
        {
            float moveX = Mathf.MoveTowards(transform.position.x, cf.target.position.x + cf.offsets.x, shakeFadeTime * 2 * Time.deltaTime);
            float moveY = Mathf.MoveTowards(transform.position.y, transformY, shakeFadeTime * 2 * Time.deltaTime);
            transform.position = new Vector3(moveX, moveY, cf.offsets.z);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            
        }
        transform.rotation = Quaternion.Euler(0, 0, shakeRotation * Random.Range(-1f, 1f));
    }

    public void StartShake(float length, float power)
    {
        shakeTimeRemaining = length;
        shakePower = power;

        shakeFadeTime = power / length;

        shakeRotation = power * rotationMulitplier;
    }

}
