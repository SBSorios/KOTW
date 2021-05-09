using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public GameObject sprite;
    public GameObject goldenLight;
    public GameObject nextNPC;
    public GameObject nextNPCAnim;
    public GameObject nextNPCTrigger;
    public GameObject cloudPuffAnim;

    public bool firstNPC;
    private bool triggered;

    private float stayTime;
    public float timer;

    private void Awake()
    {
        if (firstNPC)
        {
            sprite.SetActive(true);
            goldenLight.SetActive(true);
        }
        else
        {
            sprite.SetActive(false);
            goldenLight.SetActive(false);
        }

        stayTime = 1f;
        timer = stayTime;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            timer -= Time.deltaTime;

            if(timer <= 0 && sprite.activeInHierarchy == false)
            {
                if (!firstNPC)
                {
                    StartCoroutine(AppearInCloud());
                    timer = stayTime;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(DisappearInCloud());
            timer = stayTime;
        }
    }


    private IEnumerator DisappearInCloud()
    {
        cloudPuffAnim.GetComponent<Animator>().SetBool("Puff", true);
        yield return new WaitForSeconds(1f);
        cloudPuffAnim.GetComponent<Animator>().SetBool("Puff", false);

        if (firstNPC)
        {
            firstNPC = false;
        }

        sprite.SetActive(false);
        goldenLight.SetActive(false);

        if (nextNPC != null)
        {
            nextNPC.SetActive(true);
            nextNPCTrigger.GetComponent<NPCBehavior>().firstNPC = true;
            nextNPCTrigger.GetComponent<NPCBehavior>().goldenLight.SetActive(true);
            nextNPCAnim.GetComponent<Animator>().SetBool("Puff", true);
            yield return new WaitForSeconds(1f);
            nextNPCAnim.GetComponent<Animator>().SetBool("Puff", false);
        }
    }

    private IEnumerator AppearInCloud()
    {
        cloudPuffAnim.GetComponent<Animator>().SetBool("Puff", true);
        yield return new WaitForSeconds(1f);
        cloudPuffAnim.GetComponent<Animator>().SetBool("Puff", false);
        sprite.SetActive(true);
        goldenLight.SetActive(true);

        if(nextNPC != null)
        {
            nextNPC.SetActive(false);
            nextNPCTrigger.GetComponent<NPCBehavior>().goldenLight.SetActive(false);
        }
    }


}
