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

    public Image windIconCooldown;

    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public PlayerController pc;
    [HideInInspector]
    public PlayerHealth ph;

    public Transform startPOS;
    public Transform curCheckpoint;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            pc = player.GetComponent<PlayerController>();
            ph = player.GetComponent<PlayerHealth>();
            PlayerStart();
        }

        if (windIconCooldown != null)
        {
            windIconCooldown.fillAmount = pc.windCooldownTime;
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


    void Update()
    {

    }
}
