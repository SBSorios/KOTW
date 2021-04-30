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
    public bool l1, l2, l3, l4, bL1, bL2, bL3, bL4;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        SaveLevelUnlockData();
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
        Time.timeScale = 1;
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
                SaveManager.Instance.activeSave.levelData[i].curCollectibles = GameManager.Instance.curCollectibles;

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

                SaveManager.Instance.Save();
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
                    GameManager.Instance.curCollectibles = 0;
                    GameManager.Instance.curLevelComplete = false;
                    GameManager.Instance.curBonusUnlocked = false;
                    GameManager.Instance.ResetLives();
                    SaveManager.Instance.activeSave.activeCheckpoint = false;
                    SaveManager.Instance.activeSave.levelData[i].activeCheckpoint = false;
                    SaveManager.Instance.activeSave.levelData[i].levelComplete = false;
                    SaveManager.Instance.activeSave.spawnPosition = GameManager.Instance.startPOS.position;
                    SaveManager.Instance.activeSave.levelData[i].levelLoaded = true;
                    SaveManager.Instance.Save();
                }
                else
                {
                    GameManager.Instance.curCollectibles = SaveManager.Instance.activeSave.levelData[i].curCollectibles;
                    SaveManager.Instance.activeSave.activeCheckpoint = SaveManager.Instance.activeSave.levelData[i].activeCheckpoint;
                    if (!SaveManager.Instance.activeSave.levelData[i].activeCheckpoint)
                    {
                        GameManager.Instance.curCollectibles = 0;
                        SaveManager.Instance.activeSave.levelData[i].curCollectibles = GameManager.Instance.curCollectibles;
                        SaveManager.Instance.activeSave.spawnPosition = GameManager.Instance.startPOS.position;
                    }
                    else
                    {
                        SaveManager.Instance.activeSave.spawnPosition = GameManager.Instance.checkpointPOS.position;
                        GameManager.Instance.curCollectibles = SaveManager.Instance.activeSave.levelData[i].curCollectibles;
                    }

                    if (SaveManager.Instance.activeSave.levelData[i].levelComplete)
                    {
                        Debug.Log("Completed Level");
                        GameManager.Instance.curLevelComplete = true;
                        SaveManager.Instance.activeSave.levelData[i].levelComplete = true;
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

    public void LevelSelectReset(int index)
    {
        if (SceneManager.GetActiveScene().name == "LevelSelect")
        {
            SaveManager.Instance.activeSave.levelData[index].levelLoaded = false;
            GameManager.Instance.curCollectibles = 0;
            GameManager.Instance.curLevelComplete = false;
            GameManager.Instance.curBonusUnlocked = false;
            GameManager.Instance.ResetLives();
            SaveManager.Instance.activeSave.activeCheckpoint = false;
            SaveManager.Instance.activeSave.levelData[index].activeCheckpoint = false;
            SaveManager.Instance.activeSave.levelData[index].levelLoaded = true;
            SaveManager.Instance.Save();
        }
    }

    public void SaveLevelUnlockData()
    {
        if (SaveManager.Instance.activeSave.levelData[0].levelComplete && !l1)
        {
            l1 = true;
        }

        if (SaveManager.Instance.activeSave.levelData[1].levelComplete && !l2)
        {
            l2 = true;
        }

        if (SaveManager.Instance.activeSave.levelData[2].levelComplete && !l3)
        {
            l3 = true;
        }

        if (SaveManager.Instance.activeSave.levelData[3].levelComplete && !l4)
        {
            l4 = true;
        }

        if (SaveManager.Instance.activeSave.bonusLevels[0] && !bL1)
        {
            bL1 = true;
        }

        if (SaveManager.Instance.activeSave.bonusLevels[1] && !bL2)
        {
            bL2 = true;
        }

        if (SaveManager.Instance.activeSave.bonusLevels[2] && !bL3)
        {
            bL3 = true;
        }

        if (SaveManager.Instance.activeSave.bonusLevels[3] && !bL4)
        {
            bL4 = true;
        }
    }

}
