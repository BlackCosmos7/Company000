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
        Debug.Log("ColorRestorationManager: Start method called.");

        // ������� ����� ��� ��������������
        floorRestorationMask = new Texture2D(512, 512); // ������ ����� ����� �������� ��� ����
        wallRestorationMask = new Texture2D(512, 512); // ������ ����� ����� �������� ��� ����

        ClearRestorationMasks(); // ������� ����� �� ������

        Debug.Log("ColorRestorationManager: Masks initialized and cleared.");

        // ����������� ������ ������
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Enemy>().OnDeath += OnEnemyDeath;
            Debug.Log("ColorRestorationManager: OnDeath event subscribed for enemy " + enemy.name);
        }
    }

    private void OnEnemyDeath(GameObject enemy)
    {
        Debug.Log("ColorRestorationManager: Enemy " + enemy.name + " died.");

        // ��������� �����, ����� ������� ��� ������ �����������������
        Vector2 position = enemy.transform.position;
        UpdateRestorationMasks(position); // ��������� ��� �����
    }

    private void UpdateRestorationMasks(Vector2 position)
    {
        Debug.Log("ColorRestorationManager: Updating restoration masks for position " + position);

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

        Debug.Log("ColorRestorationManager: Masks updated and applied.");
    }

    private void Update()
    {
        // ���������, ������ �� ��� �����
        bool allEnemiesDeadTemp = true;
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                allEnemiesDeadTemp = false;
                break;
            }
        }

        if (allEnemiesDeadTemp != allEnemiesDead)
        {
            allEnemiesDead = allEnemiesDeadTemp;
            Debug.Log("ColorRestorationManager: All enemies dead status changed to " + allEnemiesDead);
        }

        // �������� �������� � ������� ��� ������ ��������������
        float restorationFactor = allEnemiesDead ? 1f : 0f;
        floorMaterial.SetFloat("_AllEnemiesDead", restorationFactor); // ��� ����
        wallMaterial.SetFloat("_AllEnemiesDead", restorationFactor);  // ��� ����

        Debug.Log("ColorRestorationManager: Restoration factor set to " + restorationFactor);
    }

    private void ClearRestorationMasks()
    {
        Debug.Log("ColorRestorationManager: Clearing restoration masks.");

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

        Debug.Log("ColorRestorationManager: Restoration masks cleared.");
    }
}

