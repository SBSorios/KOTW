using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int totalHealth;
    public int curHealth;
    public AudioClip death;

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
        StartCoroutine(Respawn());
    }

    public IEnumerator Respawn()
    {
        AudioManager.Instance.PlayClip(death);
        GameManager.Instance.player.GetComponent<SpriteRenderer>().enabled = false;
        GameManager.Instance.player.GetComponent<BoxCollider2D>().enabled = false;
        GameManager.Instance.player.GetComponentInChildren<TrailRenderer>().enabled = false;
        SaveManager.Instance.activeSave.savedTime = GameManager.Instance.elapsedTime;
        GameManager.Instance.curLives--;
        SaveManager.Instance.activeSave.lives = GameManager.Instance.curLives;
        SaveManager.Instance.Save();
        LevelManager.Instance.SaveToCurLevel();       

        yield return new WaitForSeconds(1f);

        if (GameManager.Instance.curLives <= 0)
        {
            LevelManager.Instance.LoadLevel("LoseScene");
        }
        else
        {
            LevelManager.Instance.ResetLevel();
        }
    }


}
