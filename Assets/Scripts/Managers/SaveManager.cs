using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class SaveManager : MonoBehaviour
{
    #region Instance
    static SaveManager instance;
    public static SaveManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<SaveManager>();
            }
            return instance;
        }
    }
    #endregion

    public SaveData activeSave;
    public bool freshSave;
    public bool inGame;
    public bool hasLoaded;

    void Awake()
    {
        Load();

        if (!hasLoaded)
        {
            activeSave.levelData = new List<LevelData>()
        {
            new LevelData(){levelName = "Level1", maxCollectibles = 3},
            new LevelData(){levelName = "Level2", maxCollectibles = 3},
            new LevelData(){levelName = "Level3", maxCollectibles = 3},
            new LevelData(){levelName = "Level4", maxCollectibles = 3},
            new LevelData(){levelName = "BonusLevel1", maxCollectibles = 3},
            new LevelData(){levelName = "BonusLevel2", maxCollectibles = 3},
            new LevelData(){levelName = "BonusLevel3", maxCollectibles = 3},
            new LevelData(){levelName = "BonusLevel4", maxCollectibles = 3},
        };
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            DeleteSaveData();
        }
        if (Input.GetKeyDown(KeyCode.P) && SceneManager.GetActiveScene().name == activeSave.levelData[0].levelName)
        {
            activeSave.levelData[0].levelComplete = true;
            //int index = activeSave.levelData.FindIndex()

        }
    }

    public void Save()
    {
        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();

        Debug.Log("Saved Game File At: " + stream.Name);
    }

    public void Load()
    {
        string dataPath = Application.persistentDataPath;

        if(System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();

            hasLoaded = true;

            Debug.Log("Loaded");
        }
    }

    public void DeleteSaveData()
    {
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            File.Delete(dataPath + "/" + activeSave.saveName + ".save");

            Debug.Log("Deleted Save Data");
        }
    }
}


[System.Serializable]
public class SaveData
{
    public List<LevelData> levelData;

    [Header("Save Variables")]
    public string saveName;

    [Header("Player Variables")]
    public int lives;

    [Header("Level Variables")]
    public string firstLevelName;
    public string curLevelName;
    public Vector3 spawnPosition;
    public bool activeCheckpoint;
}

[System.Serializable]
public class LevelData
{
    public string levelName;
    public float levelRating;
    public int maxCollectibles;
    public int curCollectibles;
    public Vector3 spawnPosition;
    public bool activeCheckpoint;
    public bool bonusUnlocked;
    public bool levelComplete;
}
