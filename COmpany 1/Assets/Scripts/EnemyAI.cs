using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float sightRange = 10f;          // Радиус обзора
    public float speed = 2f;                // Скорость врага
    public float attackRange = 1.5f;        // Радиус атаки
    public float attackCooldown = 1f;       // Задержка между атаками
    public LayerMask visionLayer;           // Слой для проверки видимости

    private bool isPlayerInSight = false;   // Флаг, который указывает, видит ли враг игрока
    private bool isAttacking = false;       // Флаг, указывающий, атакует ли враг
    private float lastAttackTime = 0f;      // Время последней атаки

    private Rigidbody2D rb;                 // Ссылка на Rigidbody2D для движения
    private Transform player;               // Ссылка на игрока (найдем его динамически)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Получаем компонент Rigidbody2D для движения

        // Находим игрока по тегу "Player" при старте
        player = GameObject.FindWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found in the scene. Ensure the player object has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Проверка, находится ли игрок в радиусе зрения
            CheckPlayerInSight();

            // Если игрок в поле зрения, то двигаемся к нему
            MoveTowardsPlayer();
        }
    }

    // Проверяем, находится ли игрок в радиусе зрения врага
    void CheckPlayerInSight()
    {
        if (Vector2.Distance(transform.position, player.position) <= sightRange)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, sightRange, visionLayer);

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                isPlayerInSight = true;
            }
            else
            {
                isPlayerInSight = false;
            }
        }
        else
        {
            isPlayerInSight = false;
        }
    }

    // Двигаемся в сторону игрока
    void MoveTowardsPlayer()
    {
        if (isPlayerInSight && !isAttacking)
        {
            // Направление к игроку
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed;  // Двигаемся в сторону игрока

            // Если враг достаточно близко, он начинает атаковать
            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            // Если игрок вне поля зрения, останавливаем движение врага
            rb.velocity = Vector2.zero;
        }
    }

    // Метод для атаки игрока
    private void AttackPlayer()
    {
        if (Time.time - lastAttackTime >= attackCooldown)  // Проверка на перезарядку атаки
        {
            isAttacking = true;  // Включаем флаг атаки
            lastAttackTime = Time.time;  // Обновляем время последней атаки

            Debug.Log("Enemy attacks player!");

            // Получаем скрипт игрока и наносим урон
            movePlayer playerScript = player.GetComponent<movePlayer>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(10);  // Наносим 10 единиц урона игроку
            }

            // Завершаем атаку
            isAttacking = false;
        }
    }

    // Отображение радиуса зрения и радиуса атаки в редакторе
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);  // Радиус обзора

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);  // Радиус атаки
    }
}
