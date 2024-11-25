using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float sightRange = 10f;          // ������ ������
    public float speed = 2f;                // �������� �����
    public float attackRange = 1.5f;        // ������ �����
    public float attackCooldown = 1f;       // �������� ����� �������
    public LayerMask visionLayer;           // ���� ��� �������� ���������

    private bool isPlayerInSight = false;   // ����, ������� ���������, ����� �� ���� ������
    private bool isAttacking = false;       // ����, �����������, ������� �� ����
    private float lastAttackTime = 0f;      // ����� ��������� �����

    private Rigidbody2D rb;                 // ������ �� Rigidbody2D ��� ��������
    private Transform player;               // ������ �� ������ (������ ��� �����������)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // �������� ��������� Rigidbody2D ��� ��������

        // ������� ������ �� ���� "Player" ��� ������
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
            // ��������, ��������� �� ����� � ������� ������
            CheckPlayerInSight();

            // ���� ����� � ���� ������, �� ��������� � ����
            MoveTowardsPlayer();
        }
    }

    // ���������, ��������� �� ����� � ������� ������ �����
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

    // ��������� � ������� ������
    void MoveTowardsPlayer()
    {
        if (isPlayerInSight && !isAttacking)
        {
            // ����������� � ������
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed;  // ��������� � ������� ������

            // ���� ���� ���������� ������, �� �������� ���������
            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            // ���� ����� ��� ���� ������, ������������� �������� �����
            rb.velocity = Vector2.zero;
        }
    }

    // ����� ��� ����� ������
    private void AttackPlayer()
    {
        if (Time.time - lastAttackTime >= attackCooldown)  // �������� �� ����������� �����
        {
            isAttacking = true;  // �������� ���� �����
            lastAttackTime = Time.time;  // ��������� ����� ��������� �����

            Debug.Log("Enemy attacks player!");

            // �������� ������ ������ � ������� ����
            movePlayer playerScript = player.GetComponent<movePlayer>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(10);  // ������� 10 ������ ����� ������
            }

            // ��������� �����
            isAttacking = false;
        }
    }

    // ����������� ������� ������ � ������� ����� � ���������
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);  // ������ ������

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);  // ������ �����
    }
}
