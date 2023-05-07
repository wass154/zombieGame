using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // The prefab of the zombie you want to spawn
    public float spawnRate; // The rate at which zombies will spawn
    private float spawnTimer; // The timer to keep track of when to spawn the next zombie

    void Start()
    {
        spawnTimer = spawnRate; // Set the timer to the initial spawn rate
    }

    void Update()
    {
        // Decrement the spawn timer
        spawnTimer -= Time.deltaTime;

        // If the spawn timer has reached zero, spawn a zombie and reset the timer
        if (spawnTimer <= 0)
        {
            SpawnZombie();
            spawnTimer = spawnRate;
        }
    }

    void SpawnZombie()
    {
        // Instantiate a new zombie at the position of the spawner
        Instantiate(zombiePrefab, transform.position, Quaternion.identity);
    }
}