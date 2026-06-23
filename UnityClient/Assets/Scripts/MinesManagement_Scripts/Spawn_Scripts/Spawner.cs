using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using Random = UnityEngine.Random; //booooooooh

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner
    {
        public string name;
        public SpawnerData spawnerData;    
    }

    [SerializeField] private GridController grid;
    [SerializeField] private RandomSpawner[] items;

    private void Start()
    {
        grid = FindAnyObjectByType<GridController>();
    }

    public void spawn(RandomSpawner data)
    {
        int iteration = Random.Range(data.spawnerData.min, data.spawnerData.max);

        for (int i = 0; i < iteration; i++)
        {
            int randomPosition = Random.Range(0, grid.availablePoints.Count - 1);
            GameObject spawnedItem = Instantiate(data.spawnerData.item, grid.availablePoints[randomPosition], Quaternion.identity, transform); //as GameObject??
            grid.availablePoints.RemoveAt(randomPosition);
        }
    }

    public void InitializeSpawn()
    {
        foreach(RandomSpawner item in items) 
        {
            spawn(item);
        }
    }
}
