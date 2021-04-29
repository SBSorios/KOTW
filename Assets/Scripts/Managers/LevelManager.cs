using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Instance
    static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<LevelManager>();
            }
            return instance;
        }
    }
    #endregion

    public string curLevel;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CurrentScene();
        UIManager.Instance.LoadedNewScene();
        GameManager.Instance.LoadedNewScene();

        if (scene.name == "MainMenu" || scene.name == "LevelSelect" || scene.name == "LoseScene")
        {
            UIManager.Instance.LoadedInMenus();
            SaveManager.Instance.inGame = false;
        }
        else
        {
            GameManager.Instance.LoadedInGame();
            UIManager.Instance.LoadedInGame();

            SaveManager.Instance.activeSave.lastLoadedLevel = scene.name;
            SaveManager.Instance.inGame = true;
        }

        CheckIfLevelLoaded();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CurrentScene()
    {
        curLevel = SceneManager.GetActiveScene().name;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //For When The Player Completes Level Objectives
    public void SaveToCurLevel()
    {
        for (int i = 0; i < SaveManager.Instance.activeSave.levelData.Length; i++)
        {
            if(SaveManager.Instance.activeSave.levelData[i].levelName == curLevel)
            {
                if (!SaveManager.Instance.activeSave.levelData[i].activeCheckpoint)
                {
                    SaveManager.Instance.activeSave.spawnPosition = GameManager.Instance.startPOS.position;
                }
                else
                {
                    SaveManager.Instance.activeSave.spawnPosition = GameManager.Instance.checkpointPOS.position;
                }
                SaveManager.Instance.activeSave.levelData[i].activeCheckpoint = SaveManager.Instance.activeSave.activeCheckpoint;
                SaveManager.Instance.activeSave.levelData[i].levelComplete = GameManager.Instance.curLevelComplete;
                SaveManager.Instance.activeSave.levelData[i].bonusUnlocked = GameManager.Instance.curBonusUnlocked;
                SaveManager.Instance.Save();

                if(curLevel == "Level1" && SaveManager.Instance.activeSave.levelData[i].bonusUnlocked && SaveManager.Instance.activeSave.levelData[i].activeCheckpoint)
                {
                    SaveManager.Instance.activeSave.bonusLevels[0] = true;
                }
                else if(curLevel == "Level2" && SaveManager.Instance.activeSave.levelData[i].bonusUnlocked && SaveManager.Instance.activeSave.levelData[i].activeCheckpoint)
                {
                    SaveManager.Instance.activeSave.bonusLevels[1] = true;
                }
                else if (curLevel == "Level3" && SaveManager.Instance.activeSave.levelData[i].bonusUnlocked && SaveManager.Instance.activeSave.levelData[i].activeCheckpoint)
                {
                    SaveManager.Instance.activeSave.bonusLevels[2] = true;
                }
                else if (curLevel == "Level4" && SaveManager.Instance.activeSave.levelData[i].bonusUnlocked && SaveManager.Instance.activeSave.levelData[i].activeCheckpoint)
                {
                    SaveManager.Instance.activeSave.bonusLevels[3] = true;
                }
            }
        }
    }

    //For When The Player Enters A Level
    public void CheckIfLevelLoaded()
    {
        for (int i = 0; i < SaveManager.Instance.activeSave.levelData.Length; i++)
        {
            if (SaveManager.Instance.activeSave.levelData[i].levelName == curLevel)
            {
                if(SaveManager.Instance.activeSave.levelData[i].levelLoaded == false)
                {
                    GameManager.Instance.curLevelComplete = false;
                    GameManager.Instance.curBonusUnlocked = false;
                    GameManager.Instance.ResetLives();
                    SaveManager.Instance.activeSave.activeCheckpoint = false;
                    SaveManager.Instance.activeSave.levelData[i].activeCheckpoint = false;
                    SaveManager.Instance.activeSave.spawnPosition = GameManager.Instance.startPOS.position;
                    SaveManager.Instance.activeSave.levelData[i].levelLoaded = true;
                    SaveManager.Instance.Save();
                }
                else
                {
                    SaveManager.Instance.activeSave.activeCheckpoint = SaveManager.Instance.activeSave.levelData[i].activeCheckpoint;
                    if (!SaveManager.Instance.activeSave.levelData[i].activeCheckpoint)
                    {
                        SaveManager.Instance.activeSave.spawnPosition = GameManager.Instance.startPOS.position;
                    }
                    else
                    {
                        SaveManager.Instance.activeSave.spawnPosition = GameManager.Instance.checkpointPOS.position;
                    }
                    GameManager.Instance.curLives = SaveManager.Instance.activeSave.lives;
                    GameManager.Instance.player.transform.position = SaveManager.Instance.activeSave.spawnPosition;
                    SaveManager.Instance.Save();
                }
            }
        }
    }

    //For when The Player Would Like To Retry The Level
    public void ResetLevelStats()
    {
        for (int i = 0; i < SaveManager.Instance.activeSave.levelData.Length; i++)
        {
            if (SaveManager.Instance.activeSave.levelData[i].levelName == curLevel)
            {
                SaveManager.Instance.activeSave.levelData[i].levelLoaded = false;
                CheckIfLevelLoaded();
                ResetLevel();
            }
        }
    }

}
