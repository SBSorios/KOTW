using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{

    public Button[] levelButtons;
    public GameObject loadMessage;
    public GameObject levelMessage;
    public string sceneToLoad;
    public int levelIndex;
    public Text titleText;
    public Text narrativeText;
    public Image stars;
    public GameObject startButton;
    public GameObject retryButton;
    public GameObject runImage;
    public GameObject spikeImage;
    public GameObject shockImage;
    public GameObject killImage;
    public GameObject pitfallImage;
    public GameObject vacuumImage;

    private void Awake()
    {
        levelMessage.SetActive(false);
        loadMessage.SetActive(true);
    }

    private void Update()
    {
        CheckLevelUnlocked();
    } 

    public void CheckLevelUnlocked()
    {
        if (SaveManager.Instance.activeSave.level1Unlocked)
        {
            levelButtons[1].interactable = true;
        }
        else
        {
            levelButtons[1].interactable = false;
        }

        if (SaveManager.Instance.activeSave.level2Unlocked)
        {
            levelButtons[2].interactable = true;
        }
        else
        {
            levelButtons[2].interactable = false;
        }

        if (SaveManager.Instance.activeSave.level3Unlocked)
        {
            levelButtons[3].interactable = true;
        }
        else
        {
            levelButtons[3].interactable = false;
        }

        if (SaveManager.Instance.activeSave.level4Unlocked)
        {
            levelButtons[4].interactable = true;
        }
        else
        {
            levelButtons[4].interactable = false;
        }

        if (SaveManager.Instance.activeSave.bLevel1Unlocked)
        {
            levelButtons[5].interactable = true;
        }
        else
        {
            levelButtons[5].interactable = false;
        }

        if (SaveManager.Instance.activeSave.bLevel2Unlocked)
        {
            levelButtons[6].interactable = true;
        }
        else
        {
            levelButtons[6].interactable = false;
        }

        if (SaveManager.Instance.activeSave.bLevel3Unlocked)
        {
            levelButtons[7].interactable = true;
        }
        else
        {
            levelButtons[7].interactable = false;
        }

        if (SaveManager.Instance.activeSave.bLevel4Unlocked)
        {
            levelButtons[8].interactable = true;
        }
        else
        {
            levelButtons[8].interactable = false;
        }

    }

    public void StartButton()
    {
        LevelManager.Instance.LoadLevel(sceneToLoad);
    }

    public void Tutorial()
    {
        titleText.text = "Tutorial";
        narrativeText.text = "Titan Threat Level: 0";
        sceneToLoad = "Tutorial";
        levelIndex = 0;
        levelMessage.SetActive(true);
        loadMessage.SetActive(false);

        runImage.SetActive(false);
        spikeImage.SetActive(false);
        shockImage.SetActive(false);
        killImage.SetActive(false);
        pitfallImage.SetActive(false);
        vacuumImage.SetActive(false);

        if (!SaveManager.Instance.activeSave.levelData[0].levelLoaded)
        {
            startButton.SetActive(true);
            retryButton.SetActive(false);

            stars.fillAmount = 0;
        }
        else
        {
            startButton.SetActive(false);
            retryButton.SetActive(true);

            stars.fillAmount = SaveManager.Instance.activeSave.levelData[0].levelScore / 100;
        }
    }

    public void Level1()
    {
        if (SaveManager.Instance.activeSave.levelData[0].levelComplete)
        {
            Debug.Log("Level 1 Unlocked");
            titleText.text = "The North Gate";
            narrativeText.text = "Titan Threat Level: 1";
            sceneToLoad = "Level1";
            levelIndex = 1;
            levelMessage.SetActive(true);
            loadMessage.SetActive(false);

            runImage.SetActive(true);
            spikeImage.SetActive(true);
            shockImage.SetActive(false);
            killImage.SetActive(false);
            pitfallImage.SetActive(true);
            vacuumImage.SetActive(true);

            if (!SaveManager.Instance.activeSave.levelData[1].levelLoaded)
            {
                retryButton.SetActive(false);
                startButton.SetActive(true);

                stars.fillAmount = 0;
            }
            else
            {
                startButton.SetActive(false);
                retryButton.SetActive(true);

                stars.fillAmount = SaveManager.Instance.activeSave.levelData[1].levelScore / 100;
            }
        }
    }

    public void Level2()
    {
        if (SaveManager.Instance.activeSave.levelData[1].levelComplete)
        {
            Debug.Log("Level 2 Unlocked");
            titleText.text = "The West Gate";
            narrativeText.text = "Titan Threat Level: 2";
            sceneToLoad = "Level2";
            levelIndex = 2;
            levelMessage.SetActive(true);
            loadMessage.SetActive(false);

            runImage.SetActive(true);
            spikeImage.SetActive(true);
            shockImage.SetActive(true);
            killImage.SetActive(false);
            pitfallImage.SetActive(true);
            vacuumImage.SetActive(true);

            if (!SaveManager.Instance.activeSave.levelData[2].levelLoaded)
            {
                startButton.SetActive(true);
                retryButton.SetActive(false);

                stars.fillAmount = 0;
            }
            else
            {
                startButton.SetActive(false);
                retryButton.SetActive(true);

                stars.fillAmount = SaveManager.Instance.activeSave.levelData[2].levelScore / 100;
            }
        }     
    }

    public void Level3()
    {
        if (SaveManager.Instance.activeSave.levelData[2].levelComplete)
        {
            Debug.Log("Level 3 Unlocked");
            titleText.text = "The South Gate";
            narrativeText.text = "Titan Threat Level: 3";
            sceneToLoad = "Level3";
            levelIndex = 3;
            levelMessage.SetActive(true);
            loadMessage.SetActive(false);

            runImage.SetActive(true);
            spikeImage.SetActive(true);
            shockImage.SetActive(true);
            killImage.SetActive(true);
            pitfallImage.SetActive(true);
            vacuumImage.SetActive(true);

            if (!SaveManager.Instance.activeSave.levelData[3].levelLoaded)
            {
                startButton.SetActive(true);
                retryButton.SetActive(false);

                stars.fillAmount = 0;
            }
            else
            {
                startButton.SetActive(false);
                retryButton.SetActive(true);

                stars.fillAmount = SaveManager.Instance.activeSave.levelData[3].levelScore / 100;
            }
        }
    }

    public void Level4()
    {
        if (SaveManager.Instance.activeSave.levelData[3].levelComplete)
        {
            Debug.Log("Level 4 Unlocked");
            titleText.text = "The East Gate";
            narrativeText.text = "Titan Threat Level: 4";
            sceneToLoad = "Level4";
            levelIndex = 4;
            levelMessage.SetActive(true);
            loadMessage.SetActive(false);

            runImage.SetActive(true);
            spikeImage.SetActive(true);
            shockImage.SetActive(true);
            killImage.SetActive(true);
            pitfallImage.SetActive(true);
            vacuumImage.SetActive(true);

            if (!SaveManager.Instance.activeSave.levelData[4].levelLoaded)
            {
                startButton.SetActive(true);
                retryButton.SetActive(false);

                stars.fillAmount = 0;
            }
            else
            {
                startButton.SetActive(false);
                retryButton.SetActive(true);

                stars.fillAmount = SaveManager.Instance.activeSave.levelData[4].levelScore / 100;
            }
        }
    }

    public void BonusLevel1()
    {
        if (SaveManager.Instance.activeSave.bonusLevels[0])
        {
            Debug.Log("Bonus Level 1 Unlocked");
            titleText.text = "Bonus Level 1";
            narrativeText.text = "Titan Threat Level: 1";
            sceneToLoad = "BonusLevel1";
            levelIndex = 5;
            levelMessage.SetActive(true);
            loadMessage.SetActive(false);

            runImage.SetActive(true);
            spikeImage.SetActive(true);
            shockImage.SetActive(false);
            killImage.SetActive(false);
            pitfallImage.SetActive(true);
            vacuumImage.SetActive(true);

            if (!SaveManager.Instance.activeSave.levelData[5].levelLoaded)
            {
                startButton.SetActive(true);
                retryButton.SetActive(false);

                stars.fillAmount = 0;
            }
            else
            {
                startButton.SetActive(false);
                retryButton.SetActive(true);

                stars.fillAmount = SaveManager.Instance.activeSave.levelData[5].levelScore / 100;
            }
        }
    }

    public void BonusLevel2()
    {
        Debug.Log("Bonus Level 2 Unlocked");
        titleText.text = "Bonus Level 2";
        narrativeText.text = "Titan Threat Level: 2";
        sceneToLoad = "BonusLevel2";
        levelIndex = 6;
        levelMessage.SetActive(true);
        loadMessage.SetActive(false);

        runImage.SetActive(true);
        spikeImage.SetActive(true);
        shockImage.SetActive(true);
        killImage.SetActive(false);
        pitfallImage.SetActive(true);
        vacuumImage.SetActive(true);

        if (!SaveManager.Instance.activeSave.levelData[6].levelLoaded)
        {
            startButton.SetActive(true);
            retryButton.SetActive(false);

            stars.fillAmount = 0;
        }
        else
        {
            startButton.SetActive(false);
            retryButton.SetActive(true);

            stars.fillAmount = SaveManager.Instance.activeSave.levelData[6].levelScore / 100;
        }
    }

    public void BonusLevel3()
    {
        Debug.Log("Bonus Level 3 Unlocked");
        titleText.text = "Bonus Level 3";
        narrativeText.text = "Titan Threat Level: 3";
        sceneToLoad = "BonusLevel3";
        levelIndex = 7;
        levelMessage.SetActive(true);
        loadMessage.SetActive(false);

        runImage.SetActive(true);
        spikeImage.SetActive(true);
        shockImage.SetActive(true);
        killImage.SetActive(true);
        pitfallImage.SetActive(true);
        vacuumImage.SetActive(true);


        if (!SaveManager.Instance.activeSave.levelData[7].levelLoaded)
        {
            startButton.SetActive(true);
            retryButton.SetActive(false);

            stars.fillAmount = 0;
        }
        else
        {
            startButton.SetActive(false);
            retryButton.SetActive(true);

            stars.fillAmount = SaveManager.Instance.activeSave.levelData[7].levelScore / 100;
        }
    }

    public void BonusLevel4()
    {
        Debug.Log("Bonus Level 4 Unlocked");
        titleText.text = "Bonus Level 4";
        narrativeText.text = "Titan Threat Level: 4";
        sceneToLoad = "BonusLevel4";
        levelIndex = 8;
        levelMessage.SetActive(true);
        loadMessage.SetActive(false);

        runImage.SetActive(true);
        spikeImage.SetActive(true);
        shockImage.SetActive(true);
        killImage.SetActive(true);
        pitfallImage.SetActive(true);
        vacuumImage.SetActive(true);

        if (!SaveManager.Instance.activeSave.levelData[8].levelLoaded)
        {
            startButton.SetActive(true);
            retryButton.SetActive(false);

            stars.fillAmount = 0;
        }
        else
        {
            startButton.SetActive(false);
            retryButton.SetActive(true);

            stars.fillAmount = SaveManager.Instance.activeSave.levelData[8].levelScore / 100;
        }
    }

}
