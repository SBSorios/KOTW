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
        Debug.Log("OnSceneLoaded: " + scene.name);
        UIManager.Instance.LoadedNewScene();
        GameManager.Instance.LoadedNewScene();

        if (scene.name == "MainMenu")
        {
            SaveManager.Instance.inGame = false;
            UIManager.Instance.LoadedInMenus();
        }
        else
        {
            SaveManager.Instance.activeSave.curLevelName = scene.name;
            SaveManager.Instance.inGame = true;
            GameManager.Instance.LoadedInGame();
            UIManager.Instance.LoadedInGame();
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
