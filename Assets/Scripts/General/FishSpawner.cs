using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fishToSpawn;

    [SerializeField] private float spawnTime;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float yPos;

    private void Start()
    {
        StartCoroutine(SpawnFishCo());
    }

    private IEnumerator SpawnFishCo()
    {
        yield return new WaitForSeconds(spawnTime);
        SpawnFish();
    }

    private void SpawnFish()
    {
        int fish = Random.Range(1, 3);

        for (int i = 0; i < fish; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), yPos, 0f);
            Instantiate(fishToSpawn, spawnPos, Quaternion.identity);
        }

        StartCoroutine(SpawnFishCo());
    }
}
