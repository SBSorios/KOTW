using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartConversation : MonoBehaviour
{
    private bool canSpeak;
    public Conversation conversation;

    private void Update()
    {
        BeginConvo();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            canSpeak = true;
            UIManager.Instance.infoText.GetComponent<Text>().text = "Press F To Talk";
            UIManager.Instance.ShowInfoText();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            canSpeak = false;
            UIManager.Instance.HideInfoText();
        }
    }

    void BeginConvo()
    {
        if (canSpeak)
        {
            DialogueDisplay.Instance.conversation = conversation;
            DialogueDisplay.Instance.SetSpeakers();
            if (Input.GetKeyDown(KeyCode.F))
            {
                DialogueDisplay.Instance.AdvanceConversation();
                UIManager.Instance.HideInfoText();
            }
        }
    }
}
