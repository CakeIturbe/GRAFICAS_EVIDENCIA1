using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    private float minuteToRealTime = 1f;
    private float timer;

    public GameObject zombiePrefab; // Reference to the zombie prefab
    public GameObject bossPrefab; // Reference to the boss prefab

    public GameObject boss; // Reference to the boss prefab

    // private Boss boss; // Reference to the Boss script

    public int numberOfZombiesToSpawn = 20; // Number of zombies to spawn
    public Vector3 spawnCenter = new Vector3(0, 0, -10); // Center of the spawn area
    public float spawnRadius = 70f; // Radius around the center to spawn zombies

    private bool zombiesSpawned = false; // Flag to prevent spawning more than once
    private bool bossSpawned = false; // Flag to prevent boss spawning more than once

    private List<GameObject> spawnedZombies = new List<GameObject>(); // Store references to spawned zombies

    private EnemyManager enemyManager; // Reference to the EnemyManager

    private int lastToggleMinute = -1;

    void Start()
    {
        Minute = 0;
        Hour = 0;
        timer = minuteToRealTime;

        enemyManager = FindObjectOfType<EnemyManager>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (Minute == 5 && !zombiesSpawned && Hour == 0)
        {
            SpawnZombies();
            zombiesSpawned = true;
            
        }

        if (Minute == 35 && zombiesSpawned && Hour == 0)
        {
            DestroyAllZombies();
        }

        if (Minute == 40 && !bossSpawned && Hour == 0)
        {
            
            SpawnBoss();
            bossSpawned = true;
        
        }

        

        if (bossSpawned && boss != null && Minute > 40 && Minute % 10 == 0 && Minute != lastToggleMinute)
        {
            Boss bossComponent = boss.GetComponent<Boss>();
            if (bossComponent != null)
            {
                bossComponent.ToggleState();
                lastToggleMinute = Minute;
            }
            else
            {
                Debug.LogWarning("Boss component not found on the assigned boss GameObject!");
            }
        }

        if (timer <= 0)
        {
            Minute++;
            OnMinuteChanged?.Invoke();

            if (Minute >= 60)
            {
                Hour++;
                OnHourChanged?.Invoke();
                Minute = 0;
            }

            timer = minuteToRealTime;
        }
    }

    private void SpawnZombies()
    {
        if (zombiePrefab != null)
        {
            zombiePrefab.SetActive(true);
        }
        for (int i = 0; i < numberOfZombiesToSpawn; i++)
        {
            Vector3 spawnPosition = spawnCenter + UnityEngine.Random.insideUnitSphere * spawnRadius;
            spawnPosition.y = 0; // Keep zombies on the ground level

            GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            spawnedZombies.Add(zombie); // Store the spawned zombie

            float randomSpeed = UnityEngine.Random.Range(0.4f, 20f); // Random speed between 0.5 and 20
            zombie.GetComponent<Zombie>().SetSpeed(randomSpeed);
        }
    }

    private void DestroyAllZombies()
    {
        foreach (GameObject zombie in spawnedZombies)
        {
            Destroy(zombie); // Destroy each spawned zombie
            if (enemyManager != null)
            {
                enemyManager.DecrementEnemyCount();
            }
        }
        spawnedZombies.Clear(); // Clear the list
    }

    private void SpawnBoss()
    {
        if (bossPrefab != null)
        {
            bossPrefab.SetActive(true); // Activate the pre-placed boss GameObject
            Vector3 bossSpawnPosition = bossPrefab.transform.position; // You can adjust this as needed
            boss = Instantiate(bossPrefab, bossSpawnPosition, bossPrefab.transform.rotation);

            // Check if the Boss component exists on the instantiated boss
            Boss bossComponent = boss.GetComponent<Boss>();
        }
        else
        {
            Debug.LogWarning("Boss prefab is not assigned!");
        }
    
    }
}
