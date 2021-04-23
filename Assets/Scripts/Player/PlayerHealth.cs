using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int totalHealth;
    public int curHealth;

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
        GameManager.Instance.player.GetComponent<SpriteRenderer>().enabled = false;

        GameManager.Instance.curLives--;
        SaveManager.Instance.activeSave.lives = GameManager.Instance.curLives;
        SaveManager.Instance.Save();

        yield return new WaitForSeconds(.5f);

        LevelManager.Instance.ResetLevel();
    }


}
