using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Instance
    static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }
    #endregion

    [Header("Main")]
    public Canvas mainCanvas;
    public GameObject debugPanel;
    public GameObject pausePanel;
    public GameObject optionsPanel;
    public GameObject levelCompletePanel;
    private bool debugging = false;
    private bool paused = false;

    [Header("In Game References")]
    public GameObject inGameObjects;
    public Text timerText;
    public Text torchText;
    public Text curTorchText;
    public int curTorches;
    public GameObject infoText;
    public Image windIconCooldown;
    public Image[] hearts;
    public Image[] coins;
    public Image stars;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite fullCoin;
    public Sprite emptyCoin;


    public void Update()
    {
        if (SaveManager.Instance.inGame)
        {
            HeartCounter();
            CoinCounter();
        }

        DebugMenu();
        PauseMenu();
        TorchCount();
    }

    public void FixedUpdate()
    {
        if (SaveManager.Instance.inGame)
        {
            CursorCooldown();
        }
    }

    public void LoadedNewScene()
    {
        mainCanvas = FindObjectOfType<Canvas>();
    }

    public void LoadedInMenus()
    {
        inGameObjects.SetActive(false);
    }

    public void LoadedInGame()
    {
        inGameObjects.SetActive(true);

        if (windIconCooldown != null)
        {
            windIconCooldown.fillAmount = GameManager.Instance.wc.windCooldownTime;
        }
    }

    public void HeartCounter()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < GameManager.Instance.curLives)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < GameManager.Instance.maxLives)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void CoinCounter()
    {
        for (int i = 0; i < coins.Length; i++)
        {
            if (i < GameManager.Instance.curCollectibles)
            {
                coins[i].sprite = fullCoin;
            }
            else
            {
                coins[i].sprite = emptyCoin;
            }

            if (i < SaveManager.Instance.activeSave.maxCollectibles)
            {
                coins[i].enabled = true;
            }
            else
            {
                coins[i].enabled = false;
            }
        }
    }

    private void CursorCooldown()
    {
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            mainCanvas.transform as RectTransform,
            Input.mousePosition, mainCanvas.worldCamera,
            out movePos);

        Vector3 mousePos = mainCanvas.transform.TransformPoint(movePos);

        windIconCooldown.transform.position = mousePos;

        transform.position = mousePos;
    }

    #region Menus

    public void DebugMenu()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (debugging && !paused)
            {
                debugging = false;
                debugPanel.SetActive(false);
                Time.timeScale = 1;
            }
            else if(!debugging && !paused)
            {
                debugging = true;
                debugPanel.SetActive(true);
                Time.timeScale = 0;
            }

        }
    }

    public void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!debugging && paused)
            {
                paused = false;
                pausePanel.SetActive(false);
                Time.timeScale = 1;
            }
            else if(!debugging && !paused)
            {
                paused = true;
                pausePanel.SetActive(true);
                Time.timeScale = 0;
            }

        }
    }
    #endregion

    public void ShowInfoText()
    {
        infoText.SetActive(true);
    }

    public void HideInfoText()
    {
        infoText.SetActive(false);
    }

    public void TorchCount()
    {
        curTorchText.text = curTorches.ToString();
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }
}
