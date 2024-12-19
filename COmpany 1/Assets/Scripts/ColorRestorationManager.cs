using UnityEngine;

public class ColorRestorationManager : MonoBehaviour
{
    public Material floorMaterial; // Материал для пола
    public Material wallMaterial; // Материал для стен
    public GameObject[] enemies; // Массив всех врагов
    private Texture2D floorRestorationMask; // Маска для восстановления пола
    private Texture2D wallRestorationMask; // Маска для восстановления стен
    private bool allEnemiesDead = false;

    private void Start()
    {
        // Создаем маски для восстановления
        floorRestorationMask = new Texture2D(512, 512); // Размер маски можно изменить для пола
        wallRestorationMask = new Texture2D(512, 512); // Размер маски можно изменить для стен

        ClearRestorationMasks(); // Очищаем маски на старте

        // Отслеживаем смерть врагов
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Enemy>().OnDeath += OnEnemyDeath;
        }
    }

    private void OnEnemyDeath(GameObject enemy)
    {
        // Обновляем маску, чтобы область под врагом восстанавливалась
        Vector2 position = enemy.transform.position;
        UpdateRestorationMasks(position); // Обновляем обе маски
    }

    private void UpdateRestorationMasks(Vector2 position)
    {
        // Логика обновления масок для восстановления, например, круглая область вокруг врага
        int radius = 10; // радиус восстановления для маски
        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                if (x * x + y * y <= radius * radius)
                {
                    // Обновляем маску для пола
                    floorRestorationMask.SetPixel((int)position.x + x, (int)position.y + y, Color.white);
                    // Обновляем маску для стен
                    wallRestorationMask.SetPixel((int)position.x + x, (int)position.y + y, Color.white);
                }
            }
        }
        floorRestorationMask.Apply();
        wallRestorationMask.Apply();

        // Обновляем маски в материалах
        floorMaterial.SetTexture("_FloorRestorationMask", floorRestorationMask);
        wallMaterial.SetTexture("_WallRestorationMask", wallRestorationMask);
    }

    private void Update()
    {
        // Проверяем, мертвы ли все враги
        allEnemiesDead = true;
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                allEnemiesDead = false;
                break;
            }
        }

        // Передаем параметр в шейдеры для полных восстановлений
        float restorationFactor = allEnemiesDead ? 1f : 0f;
        floorMaterial.SetFloat("_AllEnemiesDead", restorationFactor); // Для пола
        wallMaterial.SetFloat("_AllEnemiesDead", restorationFactor);  // Для стен
    }

    private void ClearRestorationMasks()
    {
        // Заполняем обе маски черным (черно-белый эффект на старте)
        for (int x = 0; x < floorRestorationMask.width; x++)
        {
            for (int y = 0; y < floorRestorationMask.height; y++)
            {
                floorRestorationMask.SetPixel(x, y, Color.black); // Маска пола
                wallRestorationMask.SetPixel(x, y, Color.black);  // Маска стен
            }
        }
        floorRestorationMask.Apply();
        wallRestorationMask.Apply();
    }
}
