using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public float timer;

    void Update()
    {
        timer -= 1 * Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }


}
