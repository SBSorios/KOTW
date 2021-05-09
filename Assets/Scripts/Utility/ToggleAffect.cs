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

    private void Update()
    {
        AllToggled();
        UIManager.Instance.torchText.text = "x " + toggles.Length.ToString();
        Debug.Log(toggles.Length);
        if (AllToggled() == true && !finishedToggling)
        {
            if(reward != null)
            {
                reward.SetActive(true);
            }
            AudioManager.Instance.PlayClip(unlockedClip);
            GameManager.Instance.curBonusUnlocked = true;
            LevelManager.Instance.SaveToCurLevel();
            finishedToggling = true;
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
