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
            Object.Destroy(gameObject);
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
            PlayerStart();
        }
    }
    #region Positioning

    public void PlayerStart()
    {
        //player.transform.position = startPOS.position;
    }

    public void CheckpointReset()
    {
        //player.transform.position = curCheckpoint.position;
    }
    #endregion

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
}
