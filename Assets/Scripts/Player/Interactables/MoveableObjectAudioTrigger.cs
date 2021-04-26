using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjectAudioTrigger : MonoBehaviour
{
    public AudioClip soundbyte;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Cursor")
        {

            AudioManager.Instance.PlayClip(soundbyte);
        }
    }
}

