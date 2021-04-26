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

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Object.Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UIManager.Instance.LoadedNewScene();
        GameManager.Instance.LoadedNewScene();

        if (scene.name == "MainMenu" || scene.name ==  "SelectSave")
        {
            UIManager.Instance.LoadedInMenus();

            SaveManager.Instance.inGame = false;
        }
        else
        {
            GameManager.Instance.LoadedInGame();
            UIManager.Instance.LoadedInGame();
            //RatingManager.Instance.LoadedInGame();

            SaveManager.Instance.activeSave.curLevelName = scene.name;
            SaveManager.Instance.Save();
            SaveManager.Instance.inGame = true;
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
