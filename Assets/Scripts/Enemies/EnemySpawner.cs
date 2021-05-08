using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public KillZone killZone;

    // Choose object to spawn in the game
    public GameObject objectToSpawn;
    public GameObject spawnerAnim;

   // Time variables
    public float minTime;
    public float maxTime;
    private float curTime;
    private float spawnTime;

    // Audio
    public AudioSource randomClip;
    public AudioClip[] audioSources;


    // Spawnpoints 
    private Transform[] spawnPoints;


    void Awake()
    {
        List<Transform> spawningPointsAsList = new List<Transform>();

        // Each child is considered a spawn point
        foreach (Transform child in transform)
        {
            spawningPointsAsList.Add(child);
        }

        Debug.Log(spawningPointsAsList);

        // Populate spawnPoints
        spawnPoints = spawningPointsAsList.ToArray();
    }


    
    void Start()
    {
        SetRandomTime();
        curTime = 0;
    }


    private void FixedUpdate()
    {

        // Count up from current time
        curTime += Time.deltaTime;


        // Check if its the right time to spawn the object
        if (curTime >= spawnTime && killZone.run)
        {
            //SpawnObject();
            StartCoroutine(Spawn());
            SetRandomTime();
            curTime = 0;
        }

    }


    void SetRandomTime()
    {
        // Set a random time between the minimum time and maximum time to spawn an object
        spawnTime = Random.Range(minTime, maxTime);
    }


    void SpawnObject()
    {
        // Reset current time
        curTime = 0;

        // Find a random index between zero and one less than the number of spawn points
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation
        Instantiate(objectToSpawn, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        // Play random sound
        RandomSound();
        

    }


    void RandomSound()
    {
        randomClip.clip = audioSources[Random.Range(0, audioSources.Length)];
        randomClip.Play();
        Debug.Log("Played Sound!");
    }

    private IEnumerator Spawn()
    {
        curTime = 0;
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(spawnerAnim, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        GameObject.FindGameObjectWithTag("Spawner").transform.parent = GameObject.FindGameObjectWithTag("KillZone").transform;
        yield return new WaitForSeconds(2);
        Destroy(GameObject.FindGameObjectWithTag("Spawner"));
        Instantiate(objectToSpawn, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        RandomSound();
    }
 
  
}
