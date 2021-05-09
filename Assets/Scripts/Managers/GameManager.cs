using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Instance
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    #endregion

    [Header("UI Variables")]
    public Camera mainCamera;

    [Header("Player Variables")]
    public int maxLives;
    public int curLives;
    public int curCollectibles;

    //[HideInInspector]
    public GameObject player;
    public GameObject titan;
    public GameObject killZone;
    [HideInInspector]
    public PlayerController pc;
    [HideInInspector]
    public PlayerHealth ph;
    [HideInInspector]
    public WindController wc;

    [Header("Gameplay Variables")]
    public Transform startPOS;
    public Transform checkpointPOS;

    private TimeSpan timePlaying;
    public float chaseLength;
    public float midTime;
    public float maxTime;
    public float elapsedTime;
    public float totalScore;
    public bool timerActive;
    public bool resetTimer;
    public bool playerStart;

    [Header("Save Variables")]
    public bool curLevelComplete;
    public bool curBonusUnlocked;


    void Awake()
    {
        #region Instance Check
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion

        if (!SaveManager.Instance.hasLoaded)
        {
            ResetLives();
        }
    }

    public void LoadedNewScene()
    {
        mainCamera = Camera.main;
    }

    public void LoadedInGame()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        titan = GameObject.FindGameObjectWithTag("Titan");
        killZone = GameObject.FindGameObjectWithTag("KillZone");
        startPOS = GameObject.FindGameObjectWithTag("Start").transform;
        checkpointPOS = GameObject.FindGameObjectWithTag("Checkpoint").transform;

        if (player != null)
        {
            pc = player.GetComponent<PlayerController>();
            ph = player.GetComponent<PlayerHealth>();
            wc = player.GetComponent<WindController>();
        }

        if (playerStart)
        {
            if (titan != null)
            {
                titan.SetActive(false);
            }
        }
    }

    #region Life
    public void AddLife()
    {
        curLives++;
        SaveManager.Instance.activeSave.lives = curLives;

    }

    public void SubtractLife()
    {
        curLives--;
        SaveManager.Instance.activeSave.lives = curLives;
    }

    public void ResetLives()
    {
        curLives = maxLives;
        SaveManager.Instance.activeSave.lives = curLives;
    }

    public void LoseCondition()
    {
        if (curLives <= 0)
        {
            LevelManager.Instance.LoadLevel("LoseScene");
        }
    }
    #endregion

    #region Game Time
    public void BeginTimer()
    {
        timerActive = true;

        if (resetTimer)
        {
            elapsedTime = 0f;
        }
        else
        {
            elapsedTime = SaveManager.Instance.activeSave.savedTime;
        }

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerActive = false;
    }

    public void DisplayElapsedTime()
    {
        timePlaying = TimeSpan.FromSeconds(SaveManager.Instance.activeSave.savedTime);
        string timePlayingText = timePlaying.ToString("mm':'ss'.'ff");
        UIManager.Instance.timerText.text = timePlayingText;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerActive)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingText = timePlaying.ToString("mm':'ss'.'ff");
            UIManager.Instance.timerText.text = timePlayingText;
            yield return null;
        }
    }
    #endregion

    #region Scoring

    public void CalculateScore()
    {
        float timeScore = 0;

        if(elapsedTime <= chaseLength)
        {
            timeScore = 33f;
        }
        else if(elapsedTime >= chaseLength + midTime)
        {
            timeScore = 16f;
        }
        else if(elapsedTime >= chaseLength + maxTime)
        {
            timeScore = 8f;
        }

        float collectibleScore = 0;

        if(curCollectibles == 3)
        {
            collectibleScore = 33f;
        }
        else if(curCollectibles == 2)
        {
            collectibleScore = 16f;
        }
        else if(curCollectibles == 1)
        {
            collectibleScore = 8f;
        }
        else if(curCollectibles == 0)
        {
            collectibleScore = 0;
        }

        float lifeScore = 0;

        if (curLives == 3)
        {
            lifeScore = 33f;
        }
        else if (curLives == 2)
        {
            lifeScore = 16f;
        }
        else if (curLives == 1)
        {
            lifeScore = 8f;
        }

        totalScore = timeScore + collectibleScore + lifeScore;
        UIManager.Instance.stars.fillAmount = totalScore / 100;
        Debug.Log(totalScore);
    }

    #endregion
}
