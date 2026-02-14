using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        Vector3[] positions = StageManager.GetEnemyPositions();
        foreach (Vector3 pos in positions)
        {
            Instantiate(enemyPrefab, pos, Quaternion.identity);
        }
    }
}
