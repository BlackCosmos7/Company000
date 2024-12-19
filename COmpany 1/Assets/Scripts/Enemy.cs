using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10f;
    public float damage = 1f;
    private bool isDead = false;

    private Renderer enemyRenderer;

    public delegate void DeathHandler(GameObject enemy);
    public event DeathHandler OnDeath;

    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        health -= amount;
        FlashRed();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;

        OnDeath?.Invoke(gameObject);

        // Уничтожаем объект сразу, без задержки
        Destroy(gameObject);
    }
    private void FlashRed()
    {
        StartCoroutine(FlashEffect());
    }

    private System.Collections.IEnumerator FlashEffect()
    {
        Color originalColor = enemyRenderer.material.color;
        enemyRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        enemyRenderer.material.color = originalColor;
    }
}
