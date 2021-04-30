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
    public GameObject startButton;
    public GameObject retryButton;

    private void Update()
    {
        CheckLevelUnlocked();
    } 

    public void CheckLevelUnlocked()
    {
        if (LevelManager.Instance.l1)
        {
            levelButtons[1].interactable = true;
        }
        else
        {
            levelButtons[1].interactable = false;
        }

        if (LevelManager.Instance.l2)
        {
            levelButtons[2].interactable = true;
        }
        else
        {
            levelButtons[2].interactable = false;
        }

        if (LevelManager.Instance.l3)
        {
            levelButtons[3].interactable = true;
        }
        else
        {
            levelButtons[3].interactable = false;
        }

        if (LevelManager.Instance.l4)
        {
            levelButtons[4].interactable = true;
        }
        else
        {
            levelButtons[4].interactable = false;
        }

        if (LevelManager.Instance.bL1)
        {
            levelButtons[5].interactable = true;
        }
        else
        {
            levelButtons[5].interactable = false;
        }

        if (LevelManager.Instance.bL2)
        {
            levelButtons[6].interactable = true;
        }
        else
        {
            levelButtons[6].interactable = false;
        }

        if (LevelManager.Instance.bL3)
        {
            levelButtons[7].interactable = true;
        }
        else
        {
            levelButtons[7].interactable = false;
        }

        if (LevelManager.Instance.bL4)
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

        if (!SaveManager.Instance.activeSave.levelData[0].levelLoaded)
        {
            startButton.SetActive(true);
            retryButton.SetActive(false);
        }
        else
        {
            startButton.SetActive(false);
            retryButton.SetActive(true);
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

            if (!SaveManager.Instance.activeSave.levelData[1].levelLoaded)
            {
                startButton.SetActive(true);
                retryButton.SetActive(false);
            }
            else
            {
                startButton.SetActive(false);
                retryButton.SetActive(true);
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

            if (!SaveManager.Instance.activeSave.levelData[2].levelLoaded)
            {
                startButton.SetActive(true);
                retryButton.SetActive(false);
            }
            else
            {
                startButton.SetActive(false);
                retryButton.SetActive(true);
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

            if (!SaveManager.Instance.activeSave.levelData[3].levelLoaded)
            {
                startButton.SetActive(true);
                retryButton.SetActive(false);
            }
            else
            {
                startButton.SetActive(false);
                retryButton.SetActive(true);
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

            if (!SaveManager.Instance.activeSave.levelData[4].levelLoaded)
            {
                startButton.SetActive(true);
                retryButton.SetActive(false);
            }
            else
            {
                startButton.SetActive(false);
                retryButton.SetActive(true);
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

            if (!SaveManager.Instance.activeSave.levelData[5].levelLoaded)
            {
                startButton.SetActive(true);
                retryButton.SetActive(false);
            }
            else
            {
                startButton.SetActive(false);
                retryButton.SetActive(true);
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

        if (!SaveManager.Instance.activeSave.levelData[6].levelLoaded)
        {
            startButton.SetActive(true);
            retryButton.SetActive(false);
        }
        else
        {
            startButton.SetActive(false);
            retryButton.SetActive(true);
        }
    }

    public void BonusLevel3()
    {
        Debug.Log("Bonus Level 3 Unlocked");
        titleText.text = "Bonus Level 3";
        narrativeText.text = "You've Unlocked A Bonus Level!";
        sceneToLoad = "BonusLevel3";
        levelIndex = 7;

        if (!SaveManager.Instance.activeSave.levelData[7].levelLoaded)
        {
            startButton.SetActive(true);
            retryButton.SetActive(false);
        }
        else
        {
            startButton.SetActive(false);
            retryButton.SetActive(true);
        }
    }

    public void BonusLevel4()
    {
        Debug.Log("Bonus Level 4 Unlocked");
        titleText.text = "Bonus Level 4";
        narrativeText.text = "You've Unlocked A Bonus Level!";
        sceneToLoad = "BonusLevel4";
        levelIndex = 8;

        if (!SaveManager.Instance.activeSave.levelData[8].levelLoaded)
        {
            startButton.SetActive(true);
            retryButton.SetActive(false);
        }
        else
        {
            startButton.SetActive(false);
            retryButton.SetActive(true);
        }
    }

}
