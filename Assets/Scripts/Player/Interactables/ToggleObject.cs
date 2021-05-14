using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    public bool toggled;
    public Animator anim;
    public AudioClip igniteClip;
    public AudioClip toggleClip;
    public Sprite toggledSprite;
    public GameObject dimLight;
    public GameObject brightLight;
    public bool torch = true;

    private void Update()
    {
        if (!toggled)
        {
            dimLight.SetActive(true);
            brightLight.SetActive(false);
        }
        else
        {
            dimLight.SetActive(false);
            brightLight.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Cursor")
        {
            if (!toggled)
            {
                if (torch)
                {
                    toggled = true;
                    anim.SetBool("Toggled", toggled);
                    UIManager.Instance.curTorches++;
                    AudioManager.Instance.PlayClip(igniteClip);
                }
                else
                {
                    toggled = true;
                    GetComponent<SpriteRenderer>().sprite = toggledSprite;
                    AudioManager.Instance.PlayClip(toggleClip);
                }
            }
        }
    }
}
