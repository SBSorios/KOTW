using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGate : MonoBehaviour
{
    private Animator anim;
    public bool opened;

    private void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();

        if (!SaveManager.Instance.activeSave.activeCheckpoint)
        {
            opened = true;
        }
        else
        {
            opened = false;
        }
    }

    private void Update()
    {
        if (opened)
        {
            anim.SetBool("Opened", true);
        }
        else
        {
            anim.SetBool("Opened", false);
        }
    }


}
