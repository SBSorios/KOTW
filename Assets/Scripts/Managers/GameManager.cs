using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public PlayerController pc;
    [HideInInspector]
    public PlayerHealth ph;
    [HideInInspector]
    public WindController wc;

    [Header("Gameplay Variables")]
    public Transform startPOS;
    public Transform curCheckpoint;
    public Vector3 respawnPoint;

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
    }

    public void LoadedNewScene()
    {
        mainCamera = Camera.main;
    }

    public void LoadedInGame()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startPOS = GameObject.FindGameObjectWithTag("Start").transform;

        if (player != null)
        {
            pc = player.GetComponent<PlayerController>();
            ph = player.GetComponent<PlayerHealth>();
            wc = player.GetComponent<WindController>();
            PlayerStart();
        }

        if (SaveManager.Instance.hasLoaded)
        {
            respawnPoint = SaveManager.Instance.activeSave.spawnPosition;
            player.transform.position = respawnPoint;
            curLives = SaveManager.Instance.activeSave.lives;
        }
        else
        {
            SaveManager.Instance.activeSave.spawnPosition = startPOS.transform.position;
            SaveManager.Instance.activeSave.lives = maxLives;
            ResetLives();
        }
    }

    public void PlayerStart()
    {
        player.transform.position = startPOS.position;
    }

    public void CheckpointReset()
    {
        player.transform.position = curCheckpoint.position;
    }

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
    }

    public void LoseCondition()
    {
        if (curLives <= 0)
        {
            LevelManager.Instance.LoadLevel("LoseScene");
            //Change Later
            SaveManager.Instance.activeSave.lives = maxLives;
            ResetLives();
        }
    }


}
