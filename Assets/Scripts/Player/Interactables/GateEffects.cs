using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateEffects : MonoBehaviour
{
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
}
