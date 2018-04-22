using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [SerializeField] private GameObject birdToSpawn;

    [SerializeField] private float spawnTime;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float yPos;

    private void Start()
    {
        StartCoroutine(SpawnBirdsCo());
    }

    private IEnumerator SpawnBirdsCo()
    {
        yield return new WaitForSeconds(spawnTime);
        SpawnBirds();
    }

    private void SpawnBirds()
    {
        int birds = Random.Range(1, 4);

        for (int i = 0; i < birds; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), yPos, 0f);
            Instantiate(birdToSpawn, spawnPos, Quaternion.identity);
        }

        StartCoroutine(SpawnBirdsCo());
    }
}
