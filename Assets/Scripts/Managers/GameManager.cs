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
            respawnPoint = SaveManager.Instance.activeSave.respawnPosition;
            player.transform.position = respawnPoint;

            ph.lives = SaveManager.Instance.activeSave.lives;
        }
        else
        {
            SaveManager.Instance.activeSave.respawnPosition = startPOS.transform.position;
            SaveManager.Instance.activeSave.lives = ph.lives;
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
}
