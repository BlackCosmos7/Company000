using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab; // ������ �����
    public Transform[] spawnPoints; // ����� ��� ������ ������
    public float spawnInterval = 5f; // �������� ����� �������� ������
    public int maxEnemies = 10; // ������������ ���������� ������ �� �����
    private int currentEnemyCount = 0; // ������� ���������� ������
    private List<bool> spawnPointsOccupied; // ������ ��� ������������ ������� ����� ������

    private void Start()
    {
        // �������������� ������ ������� ����� (��� ����� ���������� ��������)
        spawnPointsOccupied = new List<bool>(new bool[spawnPoints.Length]);

        // �������� ������������� ����� ������ SpawnEnemy � ����������
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // ���� ������� ���������� ������ ������ ��� ����� �������������, ���������� �����
        if (currentEnemyCount >= maxEnemies)
        {
            Debug.Log("������������ ���������� ������ �� �����. ����� �� ����������.");
            return;
        }

        // ���� ������ ��������� ����� ������
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!spawnPointsOccupied[i]) // ���� ����� ��������
            {
                Transform spawnPoint = spawnPoints[i]; // �������� ����� ������

                // ������ ��� ���������� �����, ��������� �����
                spawnPointsOccupied[i] = true;

                // ���������, ��� ������� ������ ����������
                Debug.Log($"���������� �� �����: {spawnPoint.position}");

                // ������� ����� � ��������� �����
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                currentEnemyCount++; // ����������� ������� ������
                Debug.Log($"���� ��������� �� ����� {spawnPoint.name}. ������� ���������� ������: {currentEnemyCount}");
                return; // ������������� ����������, ��� ��� ���� ��� ������� ���������
            }
        }

        Debug.Log("��� ��������� ����� ������, ��� ������.");
    }

    // ����� ��� ���������� �������� ���������� ������
    public void OnEnemyDeath(Transform spawnPoint)
    {
        // ��������� ���������� ������
        currentEnemyCount--;
        Debug.Log($"���� �����. ������� ���������� ������: {currentEnemyCount}");

        // ������������ ����� �� �����, ��� ��� ����� ������������� ��������
        // spawnPointsOccupied[i] = false; -- ����� ������ �� �������������
    }
}
