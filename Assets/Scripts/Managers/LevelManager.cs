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
    public bool level1Unlocked, level2Unlocked, level3Unlocked, level4Unlocked, bonusLevel1Unlocked, bonusLevel2Unlocked, bonusLevel3Unlocked, bonusLevel4Unlocked;
    private LevelSelect levelSelect;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        //SaveLevelUnlockData();

        if (level1Unlocked)
        {

        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CurrentScene();
        UIManager.Instance.LoadedNewScene();
        GameManager.Instance.LoadedNewScene();

        if(scene.name == "BufferScene")
        {
            UIManager.Instance.fadeImage.SetActive(true);
            StartCoroutine(UIManager.Instance.DelinquentsIntro());
        }
        else
        {
            UIManager.Instance.fadeImage.SetActive(false);
        }

        if (scene.name == "MainMenu" || scene.name == "LevelSelect" || scene.name == "LoseScene" || scene.name == "IntroCutscene" || scene.name == "BufferScene" 
            || scene.name == "WinScene" || scene.name == "CreditsScene")
        {
            UIManager.Instance.LoadedInMenus();
            SaveManager.Instance.inGame = false;

            if(scene.name == "LevelSelect")
            {
                levelSelect = GameObject.FindGameObjectWithTag("Level Select").GetComponent<LevelSelect>();
            }
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
        Debug.Log("Quit Game");
    }

    //For When The Player Completes Level Objectives
    public void SaveToCurLevel()
    {
        for (int i = 0; i < SaveManager.Instance.activeSave.levelData.Length; i++)
        {
            if(SaveManager.Instance.activeSave.levelData[i].levelName == curLevel)
            {
                SaveManager.Instance.activeSave.levelData[i].curCollectibles = GameManager.Instance.curCollectibles;
                SaveManager.Instance.activeSave.levelData[i].levelTime = SaveManager.Instance.activeSave.savedTime;
                SaveManager.Instance.activeSave.levelData[i].levelScore = GameManager.Instance.totalScore;
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

                if(curLevel == "Level1" && SaveManager.Instance.activeSave.levelData[i].bonusUnlocked && SaveManager.Instance.activeSave.levelData[i].levelComplete)
                {
                    SaveManager.Instance.activeSave.bLevel1Unlocked = true;
                }
                else if(curLevel == "Level2" && SaveManager.Instance.activeSave.levelData[i].bonusUnlocked && SaveManager.Instance.activeSave.levelData[i].levelComplete)
                {
                    SaveManager.Instance.activeSave.bLevel2Unlocked = true;
                }
                else if (curLevel == "Level3" && SaveManager.Instance.activeSave.levelData[i].bonusUnlocked && SaveManager.Instance.activeSave.levelData[i].levelComplete)
                {
                    SaveManager.Instance.activeSave.bLevel3Unlocked = true;
                }
                else if (curLevel == "Level4" && SaveManager.Instance.activeSave.levelData[i].bonusUnlocked && SaveManager.Instance.activeSave.levelData[i].levelComplete)
                {
                    SaveManager.Instance.activeSave.bLevel4Unlocked = true;
                }

                if (curLevel == "Tutorial" && GameManager.Instance.curLevelComplete)
                {
                    SaveManager.Instance.activeSave.level1Unlocked = true;
                }

                if (curLevel == "Level1" && GameManager.Instance.curLevelComplete)
                {
                    SaveManager.Instance.activeSave.level2Unlocked = true;
                }

                if (curLevel == "Level2" && GameManager.Instance.curLevelComplete)
                {
                    SaveManager.Instance.activeSave.level3Unlocked = true;
                }

                if (curLevel == "Level3" && GameManager.Instance.curLevelComplete)
                {
                    SaveManager.Instance.activeSave.level4Unlocked = true;
                }

                if (curLevel == "Level4" && GameManager.Instance.curLevelComplete)
                {
                    SaveManager.Instance.activeSave.level4Unlocked = true;
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
                    //GameManager.Instance.curLevelComplete = false;
                    GameManager.Instance.curBonusUnlocked = false;
                    GameManager.Instance.ResetLives();
                    SaveManager.Instance.activeSave.activeCheckpoint = false;
                    SaveManager.Instance.activeSave.levelData[i].activeCheckpoint = false;
                    SaveManager.Instance.activeSave.levelData[i].levelComplete = false;
                    SaveManager.Instance.activeSave.savedTime = 0;
                    SaveManager.Instance.activeSave.levelData[i].levelTime = 0;
                    SaveManager.Instance.activeSave.levelData[i].levelScore = 0;
                    SaveManager.Instance.activeSave.spawnPosition = GameManager.Instance.startPOS.position;
                    SaveManager.Instance.activeSave.levelData[i].levelLoaded = true;
                    SaveManager.Instance.Save();
                }
                else
                {
                    GameManager.Instance.curCollectibles = SaveManager.Instance.activeSave.levelData[i].curCollectibles;
                    SaveManager.Instance.activeSave.savedTime = SaveManager.Instance.activeSave.levelData[i].levelTime;
                    GameManager.Instance.totalScore = SaveManager.Instance.activeSave.levelData[i].levelScore;
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

    public void LevelSelectReset()
    {
        int index = levelSelect.levelIndex;
        Debug.Log(index);
        SaveManager.Instance.activeSave.levelData[index].levelLoaded = false;
        GameManager.Instance.curCollectibles = 0;
        //GameManager.Instance.curLevelComplete = false;
        GameManager.Instance.curBonusUnlocked = false;
        SaveManager.Instance.activeSave.savedTime = 0;
        SaveManager.Instance.activeSave.levelData[index].levelTime = 0;
        SaveManager.Instance.activeSave.levelData[index].levelScore = 0;
        GameManager.Instance.ResetLives();
        SaveManager.Instance.activeSave.activeCheckpoint = false;
        SaveManager.Instance.activeSave.levelData[index].activeCheckpoint = false;
        SaveManager.Instance.activeSave.levelData[index].levelComplete = false;
        SaveManager.Instance.Save();
    }

    public void SaveLevelUnlockData()
    {
        if (SaveManager.Instance.activeSave.levelData[0].levelLoaded && !level1Unlocked)
        {
            level1Unlocked = true;
        }

        if (SaveManager.Instance.activeSave.levelData[1].levelLoaded && !level2Unlocked)
        {
            level2Unlocked = true;
        }

        if (SaveManager.Instance.activeSave.levelData[2].levelLoaded && !level3Unlocked)
        {
            level3Unlocked = true;
        }

        if (SaveManager.Instance.activeSave.levelData[3].levelComplete && !level4Unlocked)
        {
            level4Unlocked = true;
        }

        if (SaveManager.Instance.activeSave.bonusLevels[0] && !bonusLevel1Unlocked)
        {
            bonusLevel1Unlocked = true;
        }

        if (SaveManager.Instance.activeSave.bonusLevels[1] && !bonusLevel2Unlocked)
        {
            bonusLevel2Unlocked = true;
        }

        if (SaveManager.Instance.activeSave.bonusLevels[2] && !bonusLevel3Unlocked)
        {
            bonusLevel3Unlocked = true;
        }

        if (SaveManager.Instance.activeSave.bonusLevels[3] && !bonusLevel4Unlocked)
        {
            bonusLevel4Unlocked = true;
        }
    }

}
