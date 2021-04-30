using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    public bool newGame;
    public Button startGameButton;
    public GameObject deleteSaveButton;

    public GameObject messageHolder;

    private void Update()
    {
        if (!SaveManager.Instance.hasLoaded)
        {
            startGameButton.GetComponentInChildren<Text>().text = "New Game";
            deleteSaveButton.SetActive(false);
            newGame = true;
        }
        else
        {
            startGameButton.GetComponentInChildren<Text>().text = "Load Game";
            deleteSaveButton.SetActive(true);
            newGame = false;
        }
    }

    public void StartGame()
    {
        LevelManager.Instance.LoadLevel("LevelSelect");
    }

    public void MessageShow()
    {
        messageHolder.SetActive(true);
    }

    public void MessageHide()
    {
        messageHolder.SetActive(false);
    }

    public void DeleteSave()
    {
        SaveManager.Instance.DeleteSaveData();
        SaveManager.Instance.hasLoaded = false;
        newGame = true;
    }


}
