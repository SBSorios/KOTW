using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAffect : MonoBehaviour
{
    public ToggleObject[] toggles;
    public GameObject reward;
    public AudioClip unlockedClip;

    private int curToggled;
    private bool finishedToggling;
    public bool bonus = true;

    private void Update()
    {
        AllToggled();
        if (bonus)
        {
            UIManager.Instance.torchText.text = "x " + toggles.Length.ToString();
        }

        if (AllToggled() == true && !finishedToggling)
        {
            if(reward != null)
            {
                reward.SetActive(true);
            }

            if (bonus)
            {
                AudioManager.Instance.PlayClip(unlockedClip);
                GameManager.Instance.curBonusUnlocked = true;
                LevelManager.Instance.SaveToCurLevel();
                finishedToggling = true;
            }
            else
            {
                GetComponentInChildren<Animator>().SetBool("Toggled", true);
                finishedToggling = true;
            }
        }
    }

    public bool AllToggled()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (!toggles[i].toggled)
            {
                return false;
            }
        }
        return true;
    }
}
