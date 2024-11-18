using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float sightRange = 10f;
    public float speed = 2f;
    public float attackRange = 1.5f;
    public LayerMask visionLayer;

    private bool isPlayerInSight = false;
    private bool isAttacking = false;

    public delegate void PlayerDetected();
    public static event PlayerDetected OnPlayerDetected;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInSight = true;
            OnPlayerDetected?.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInSight = false;
        }
    }

    public void MoveTowardsPlayer()
    {
        if (isPlayerInSight && !isAttacking)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                AttackPlayer();
            }
        }
    }

    private void AttackPlayer()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            Debug.Log("Enemy attacks player!");
            Player playerScript = player.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(10f);
            }
            isAttacking = false;
        }
    }
}

