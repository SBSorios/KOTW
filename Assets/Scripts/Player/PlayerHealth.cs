using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private GameObject player;

    public int totalHealth;
    public int curHealth;

    void Start()
    {
        player = GameManager.Instance.player;
        curHealth = totalHealth;
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
            if(GameManager.Instance.curCheckpoint == null)
            {
                Debug.Log("Reset Level");
                LevelManager.Instance.ResetLevel();
            }
            else
            {
                Debug.Log("Reset Player At Checkpoint");
                curHealth = totalHealth;
                GameManager.Instance.CheckpointReset();
            }
        }
    }


}
