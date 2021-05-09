using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDisplay : MonoBehaviour
{
    #region Instance
    static DialogueDisplay instance;
    public static DialogueDisplay Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<DialogueDisplay>();
            }
            return instance;
        }
    }
    #endregion

    public Conversation conversation;

    public GameObject speakerLeft;
    public GameObject speakerRight;

    private SpeakerUI speakerUILeft;
    private SpeakerUI speakerUIRight;

    private int activeLineIndex = 0;

    private void Start()
    {
        speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUI>();

        if(conversation != null)
        {
            speakerUILeft.Speaker = conversation.speakerLeft;
            speakerUIRight.Speaker = conversation.speakerRight;
        }
    }

    public void AdvanceConversation()
    {
        if(activeLineIndex < conversation.lines.Length)
        {
            DisplayLine();
            activeLineIndex += 1;
            GameManager.Instance.pc.canMove = false;
            GameManager.Instance.wc.canCast = false;
        }
        else
        {
            speakerUILeft.Hide();
            speakerUIRight.Hide();
            activeLineIndex = 0;
            GameManager.Instance.pc.canMove = true;
            GameManager.Instance.wc.canCast = true;
        }
    }

    void DisplayLine()
    {
        Line line = conversation.lines[activeLineIndex];
        Character character = line.character;

        if (speakerUILeft.SpeakerIs(character))
        {
            SetDialogue(speakerUILeft, speakerUIRight, line.text);
            speakerUIRight.Hide();
        }
        else
        {
            SetDialogue(speakerUIRight, speakerUILeft, line.text);
            speakerUILeft.Hide();
        }
    }

    void SetDialogue(SpeakerUI activeSpeakerUI, SpeakerUI inactiveSpeakerUI, string text) 
    {
        activeSpeakerUI.Dialogue = text;
        activeSpeakerUI.Show();
    }

    public void SetSpeakers()
    {
        speakerUILeft.Speaker = conversation.speakerLeft;
        speakerUIRight.Speaker = conversation.speakerRight;
    }
}
