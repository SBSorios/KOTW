using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{

    public Button[] levelButtons;
    public GameObject levelMessage;
    public string sceneToLoad;
    public int levelIndex;
    public Text titleText;
    public Text narrativeText;
    public Image stars;
    public GameObject startButton;
    public GameObject retryButton;

    private void Awake()
    {
        levelMessage.SetActive(false);
    }

    private void Update()
    {
        CheckLevelUnlocked();
    } 

    public void CheckLevelUnlocked()
    {
        if (LevelManager.Instance.level1Unlocked)
        {
            levelButtons[1].interactable = true;
        }
        else
        {
            levelButtons[1].interactable = false;
        }

        if (LevelManager.Instance.level2Unlocked)
        {
            levelButtons[2].interactable = true;
        }
        else
        {
            levelButtons[2].interactable = false;
        }

        if (LevelManager.Instance.level3Unlocked)
        {
            levelButtons[3].interactable = true;
        }
        else
        {
            levelButtons[3].interactable = false;
        }

        if (LevelManager.Instance.level4Unlocked)
        {
            levelButtons[4].interactable = true;
        }
        else
        {
            levelButtons[4].interactable = false;
        }

        if (LevelManager.Instance.bonusLevel1Unlocked)
        {
            levelButtons[5].interactable = true;
        }
        else
        {
            levelButtons[5].interactable = false;
        }

        if (LevelManager.Instance.bonusLevel2Unlocked)
        {
            levelButtons[6].interactable = true;
        }
        else
        {
            levelButtons[6].interactable = false;
        }

        if (LevelManager.Instance.bonusLevel3Unlocked)
        {
            levelButtons[7].interactable = true;
        }
        else
        {
            levelButtons[7].interactable = false;
        }

        if (LevelManager.Instance.bonusLevel4Unlocked)
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
        narrativeText.text = "Basic story explained here...";
        sceneToLoad = "Tutorial";
        levelIndex = 0;
        levelMessage.SetActive(true);

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
            titleText.text = "Level 1 \n The North Gate";
            narrativeText.text = "Level 1 story explained here...";
            sceneToLoad = "Level1";
            levelIndex = 1;
            levelMessage.SetActive(true);

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
            titleText.text = "Level 2 \n The West Gate";
            narrativeText.text = "Level 2 story explained here...";
            sceneToLoad = "Level2";
            levelIndex = 2;
            levelMessage.SetActive(true);

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
            titleText.text = "Level 3 \n The South Gate";
            narrativeText.text = "Level 3 story explained here...";
            sceneToLoad = "Level3";
            levelIndex = 3;
            levelMessage.SetActive(true);

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
            titleText.text = "Level 4 \n The East Gate";
            narrativeText.text = "Level 4 story explained here...";
            sceneToLoad = "Level4";
            levelIndex = 4;
            levelMessage.SetActive(true);

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
            narrativeText.text = "You've Unlocked A Bonus Level!";
            sceneToLoad = "BonusLevel1";
            levelIndex = 5;
            levelMessage.SetActive(true);

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
        narrativeText.text = "You've Unlocked A Bonus Level!";
        sceneToLoad = "BonusLevel2";
        levelIndex = 6;
        levelMessage.SetActive(true);

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
        narrativeText.text = "You've Unlocked A Bonus Level!";
        sceneToLoad = "BonusLevel3";
        levelIndex = 7;
        levelMessage.SetActive(true);

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
        narrativeText.text = "You've Unlocked A Bonus Level!";
        sceneToLoad = "BonusLevel4";
        levelIndex = 8;
        levelMessage.SetActive(true);

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
