using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAffect : MonoBehaviour
{
    public ToggleObject[] toggles;
    public GameObject reward;

    private void Update()
    {
        AllToggled();

        if (AllToggled() == true)
        {
            reward.SetActive(true);
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
