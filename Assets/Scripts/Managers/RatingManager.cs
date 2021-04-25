using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingManager : MonoBehaviour
{
    #region Instance
    static RatingManager instance;
    public static RatingManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<RatingManager>();
            }
            return instance;
        }
    }
    #endregion

    private int totalCoins;

    public void LoadedInGame()
    {
        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
    }

    public void StarsAchieved()
    {
        int curCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        int coinsCollected = totalCoins - curCoins;
    }
}
