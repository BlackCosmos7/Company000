using UnityEngine;

public class ColorRestorationManager : MonoBehaviour
{
    public Material floorMaterial; // �������� ��� ����
    public Material wallMaterial; // �������� ��� ����
    public GameObject[] enemies; // ������ ���� ������
    private Texture2D floorRestorationMask; // ����� ��� �������������� ����
    private Texture2D wallRestorationMask; // ����� ��� �������������� ����
    private bool allEnemiesDead = false;

    private void Start()
    {
        // ������� ����� ��� ��������������
        floorRestorationMask = new Texture2D(512, 512); // ������ ����� ����� �������� ��� ����
        wallRestorationMask = new Texture2D(512, 512); // ������ ����� ����� �������� ��� ����

        ClearRestorationMasks(); // ������� ����� �� ������

        // ����������� ������ ������
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Enemy>().OnDeath += OnEnemyDeath;
        }
    }

    private void OnEnemyDeath(GameObject enemy)
    {
        // ��������� �����, ����� ������� ��� ������ �����������������
        Vector2 position = enemy.transform.position;
        UpdateRestorationMasks(position); // ��������� ��� �����
    }

    private void UpdateRestorationMasks(Vector2 position)
    {
        // ������ ���������� ����� ��� ��������������, ��������, ������� ������� ������ �����
        int radius = 10; // ������ �������������� ��� �����
        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                if (x * x + y * y <= radius * radius)
                {
                    // ��������� ����� ��� ����
                    floorRestorationMask.SetPixel((int)position.x + x, (int)position.y + y, Color.white);
                    // ��������� ����� ��� ����
                    wallRestorationMask.SetPixel((int)position.x + x, (int)position.y + y, Color.white);
                }
            }
        }
        floorRestorationMask.Apply();
        wallRestorationMask.Apply();

        // ��������� ����� � ����������
        floorMaterial.SetTexture("_FloorRestorationMask", floorRestorationMask);
        wallMaterial.SetTexture("_WallRestorationMask", wallRestorationMask);
    }

    private void Update()
    {
        // ���������, ������ �� ��� �����
        allEnemiesDead = true;
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                allEnemiesDead = false;
                break;
            }
        }

        // �������� �������� � ������� ��� ������ ��������������
        float restorationFactor = allEnemiesDead ? 1f : 0f;
        floorMaterial.SetFloat("_AllEnemiesDead", restorationFactor); // ��� ����
        wallMaterial.SetFloat("_AllEnemiesDead", restorationFactor);  // ��� ����
    }

    private void ClearRestorationMasks()
    {
        // ��������� ��� ����� ������ (�����-����� ������ �� ������)
        for (int x = 0; x < floorRestorationMask.width; x++)
        {
            for (int y = 0; y < floorRestorationMask.height; y++)
            {
                floorRestorationMask.SetPixel(x, y, Color.black); // ����� ����
                wallRestorationMask.SetPixel(x, y, Color.black);  // ����� ����
            }
        }
        floorRestorationMask.Apply();
        wallRestorationMask.Apply();
    }
}
