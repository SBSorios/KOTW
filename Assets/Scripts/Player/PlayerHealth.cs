using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int lives;

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

        lives--;
        SaveManager.Instance.activeSave.lives = lives;
        SaveManager.Instance.Save();

        yield return new WaitForSeconds(.5f);

        LevelManager.Instance.ResetLevel();
    }


}
