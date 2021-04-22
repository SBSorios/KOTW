using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private GameObject player;

    public int totalLives;
    public int curLives;

    public int totalHealth;
    public int curHealth;

    void Start()
    {
        player = GameManager.Instance.player;
        curHealth = totalHealth;
        curLives = totalLives;

        PlayerPrefs.SetInt("Current Lives", curLives);
    }

    private void Update()
    {
        Death();
    }

    public void Heal(int amount)
    {
        curHealth += amount;

    }

    public void Damage(int amount)
    {
        curHealth -= amount;
    }

    public void Death()
    {
        if(curHealth <= 0)
        {
            Debug.Log("Player Dead");
            PlayerPrefs.SetInt("Current Lives", curLives--);

            if (GameManager.Instance.curCheckpoint == null)
            {
                Debug.Log("Total Lives Left: " + PlayerPrefs.GetInt("Current Lives"));
                LevelManager.Instance.ResetLevel();
            }
            else
            {
                Debug.Log("Total Lives Left: " + PlayerPrefs.GetInt("Current Lives"));
                curHealth = totalHealth;
                GameManager.Instance.CheckpointReset();
            }
        }
    }


}
