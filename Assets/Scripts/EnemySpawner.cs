using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemyCount = 20;
    [SerializeField] private float minX = 5f;
    [SerializeField] private float maxX = 45f;
    [SerializeField] private float minDistance = 3f; // ìGìØémÇÃç≈è¨ä‘äu

    private List<Vector3> spawnedPositions = new List<Vector3>();

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPos = GetValidSpawnPosition();
            spawnedPositions.Add(spawnPos);
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }

    Vector3 GetValidSpawnPosition()
    {
        int maxAttempts = 30;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            float randomX = Random.Range(minX, maxX);
            Vector3 candidate = new Vector3(randomX, 0, 0);

            // ëºÇÃìGÇ∆ãóó£É`ÉFÉbÉN
            bool isFarEnough = true;
            foreach (Vector3 pos in spawnedPositions)
            {
                if (Vector3.Distance(candidate, pos) < minDistance)
                {
                    isFarEnough = false;
                    break;
                }
            }

            if (isFarEnough)
            {
                return candidate;
            }
        }

        // å©Ç¬Ç©ÇÁÇ»ÇØÇÍÇŒÉâÉìÉ_ÉÄÇ≈ë√ã¶
        return new Vector3(Random.Range(minX, maxX), 0, 0);
    }
}