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
    public bool inGame;
    public bool hasLoaded;

    void Awake()
    {
        Load();
        activeSave.maxCollectibles = 3;

        if (!hasLoaded)
        {
            activeSave.levelData[0].levelName = "Tutorial";
            activeSave.levelData[1].levelName = "Level1";
            activeSave.levelData[2].levelName = "Level2";
            activeSave.levelData[3].levelName = "Level3";
            activeSave.levelData[4].levelName = "Level4";
            activeSave.levelData[5].levelName = "BonusLevel1";
            activeSave.levelData[6].levelName = "BonusLevel2";
            activeSave.levelData[7].levelName = "BonusLevel3";
            activeSave.levelData[8].levelName = "BonusLevel4";
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
    public LevelData[] levelData;
    public bool[] bonusLevels;

    [Header("Save Variables")]
    public string saveName;

    [Header("Player Variables")]
    public int lives;

    [Header("Level Variables")]
    public string firstLevelName;
    public string lastLoadedLevel;
    public float savedTime;
    public int maxCollectibles;
    public Vector3 spawnPosition;
    public bool activeCheckpoint;
}

[System.Serializable]
public class LevelData
{
    public string levelName;
    public float levelTime;
    public float levelScore;
    public int curCollectibles;
    public Vector3 spawnPosition;
    public bool activeCheckpoint;
    public bool bonusUnlocked;
    public bool levelLoaded;
    public bool levelComplete;
}
