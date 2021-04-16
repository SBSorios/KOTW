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

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();

        windIconCooldown.fillAmount = pc.windCooldownTime;
    }


    void Update()
    {
        
    }
}
