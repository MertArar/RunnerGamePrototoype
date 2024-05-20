using System.Collections.Generic;
using UnityEngine;

public class CloneObject : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;  // Engeller için prefabler
    public GameObject coinPrefab;  // Coin prefab
    public float spawnDistance = 50f;  // Spawn mesafesi
    public Transform playerTransform;  // Karakterin transformu
    public string[] tileTags = { "City", "Desert" };  // Tile tagleri
    public float minObstacleDistance = 15f;  // Engeller arası minimum mesafe
    public float coinSpacing = 1f;  // Coinler arası mesafe
    public int minCoinCount = 7;  // Minimum coin sayısı
    public int maxCoinCount = 15;  // Maksimum coin sayısı

    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Update()
    {
        // Karakterin önündeki pozisyon
        Vector3 spawnPosition = playerTransform.position + Vector3.forward * spawnDistance;

        // Objeleri spawn etmek için tag kontrolü
        foreach (string tag in tileTags)
        {
            GameObject[] tiles = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject tile in tiles)
            {
                if (Vector3.Distance(tile.transform.position, spawnPosition) < spawnDistance)
                {
                    SpawnObstacles(spawnPosition);
                    SpawnCoins(spawnPosition);
                }
            }
        }

        // Karakterin arkasında kalan objeleri yok et
        DestroyObjectsBehindPlayer();
    }

    void SpawnObstacles(Vector3 spawnPosition)
    {
        float[] xPositions = { -2f, 0f, 2f };

        foreach (float x in xPositions)
        {
            Vector3 position = new Vector3(x, spawnPosition.y, spawnPosition.z);

            if (!IsPositionOccupied(position, minObstacleDistance))
            {
                int randomIndex = Random.Range(0, obstaclePrefabs.Length);
                GameObject obstacle = Instantiate(obstaclePrefabs[randomIndex], position, Quaternion.identity);
                spawnedObjects.Add(obstacle);
            }
        }
    }

    void SpawnCoins(Vector3 spawnPosition)
    {
        int coinCount = Random.Range(minCoinCount, maxCoinCount);
        float[] xPositions = { -2f, 0f, 2f };
        float startZ = spawnPosition.z;

        for (int i = 0; i < coinCount; i++)
        {
            Vector3 position = new Vector3(
                xPositions[Random.Range(0, xPositions.Length)], 
                spawnPosition.y, 
                startZ + i * coinSpacing
            );

            if (!IsPositionOccupied(position, coinSpacing))
            {
                GameObject coin = Instantiate(coinPrefab, position, Quaternion.identity);
                spawnedObjects.Add(coin);
            }
        }
    }

    bool IsPositionOccupied(Vector3 position, float minDistance)
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (Vector3.Distance(position, obj.transform.position) < minDistance)
            {
                return true;
            }
        }
        return false;
    }

    void DestroyObjectsBehindPlayer()
    {
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (spawnedObjects[i].transform.position.z < playerTransform.position.z)
            {
                Destroy(spawnedObjects[i]);
                spawnedObjects.RemoveAt(i);
            }
        }
    }
}
