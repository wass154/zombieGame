using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public int poolSize = 10;

    private List<GameObject> zombiePool = new List<GameObject>();

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject zombie = Instantiate(zombiePrefab, transform.position, Quaternion.identity);
            zombie.SetActive(false);
            zombiePool.Add(zombie);
        }
    }

    public GameObject GetZombie()
    {
        for (int i = 0; i < zombiePool.Count; i++)
        {
            if (!zombiePool[i].activeInHierarchy)
            {
                return zombiePool[i];
            }
        }

        return null;
    }

    public void SpawnZombie(Vector3 position)
    {
        GameObject zombie = GetZombie();

        if (zombie == null)
        {
            Debug.LogError("Zombie pool is empty!");
            return;
        }

        zombie.transform.position = position;
        zombie.SetActive(true);
    }

    public void AddZombieToPool(GameObject zombie)
    {
        zombie.SetActive(false);
        zombiePool.Add(zombie);
    }
}