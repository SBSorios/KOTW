using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSave : MonoBehaviour
{
    //Switch over to input field when button pressed, then save
    //Add in heart images to show how many lives player has
    //Add in current level
    //Add in play time

    public GameObject pressedButton;
    public InputField slotInputField;
    public Text slotText;

    public void SelectSaveSlot(string level)
    {
        pressedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        if (!pressedButton.GetComponent<SaveSlot>().newGame)
        {
            slotText = pressedButton.GetComponentInChildren<Text>();
            slotText.text = "Saved";

            SaveManager.Instance.Save();
            pressedButton.GetComponent<SaveSlot>().newGame = true;
        }
        else
        {
            LevelManager.Instance.LoadLevel(level);
        }
    }

    public void DeleteSaveSlot()
    {
        if(pressedButton != null)
        {
            slotText.text = "New Save";

            SaveManager.Instance.DeleteSaveData();
        }
    }

}
