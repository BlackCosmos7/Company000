using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Префаб врага
    public Transform[] spawnPoints; // Точки для спавна врагов
    public float spawnInterval = 5f; // Интервал между спавнами врагов

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        if (spawnPoint.childCount == 0)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
