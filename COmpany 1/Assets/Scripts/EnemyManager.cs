using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Префаб врага
    public Transform[] spawnPoints; // Точки для спавна врагов
    public float spawnInterval = 5f; // Интервал между спавнами врагов
    public int maxEnemies = 10; // Максимальное количество врагов на сцене
    private int currentEnemyCount = 0; // Текущее количество врагов
    private List<bool> spawnPointsOccupied; // Список для отслеживания занятых точек спавна

    private void Start()
    {
        // Инициализируем список занятых точек (все точки изначально свободны)
        spawnPointsOccupied = new List<bool>(new bool[spawnPoints.Length]);

        // Начинаем повторяющийся вызов метода SpawnEnemy с интервалом
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Если текущее количество врагов больше или равно максимальному, прекращаем спавн
        if (currentEnemyCount >= maxEnemies)
        {
            Debug.Log("Максимальное количество врагов на сцене. Спавн не происходит.");
            return;
        }

        // Ищем первую свободную точку спавна
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!spawnPointsOccupied[i]) // Если точка свободна
            {
                Transform spawnPoint = spawnPoints[i]; // Получаем точку спавна

                // Прежде чем заспавнить врага, блокируем точку
                spawnPointsOccupied[i] = true;

                // Проверяем, что позиция спавна правильная
                Debug.Log($"Заспавнено на точке: {spawnPoint.position}");

                // Создаем врага в выбранной точке
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                currentEnemyCount++; // Увеличиваем счётчик врагов
                Debug.Log($"Враг заспавнен на точке {spawnPoint.name}. Текущее количество врагов: {currentEnemyCount}");
                return; // Останавливаем выполнение, так как враг был успешно заспавнен
            }
        }

        Debug.Log("Нет доступных точек спавна, все заняты.");
    }

    // Метод для уменьшения текущего количества врагов
    public void OnEnemyDeath(Transform spawnPoint)
    {
        // Уменьшаем количество врагов
        currentEnemyCount--;
        Debug.Log($"Враг погиб. Текущее количество врагов: {currentEnemyCount}");

        // Освобождение точки не нужно, так как точка заблокирована навсегда
        // spawnPointsOccupied[i] = false; -- точка больше не освобождается
    }
}
