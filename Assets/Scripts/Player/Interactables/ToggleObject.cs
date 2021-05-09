using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    public bool toggled;
    public Animator anim;
    public AudioClip igniteClip;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Cursor")
        {
            if (!toggled)
            {
                toggled = true;
                anim.SetBool("Toggled", toggled);
                UIManager.Instance.curTorches++;
                AudioManager.Instance.PlayClip(igniteClip);
            }
        }
    }
}
