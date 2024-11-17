using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float damage = 10f;
    private bool isDead = false;

    private Renderer enemyRenderer;
    private Animator enemyAnimator;

    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        enemyAnimator = GetComponent<Animator>();
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
        enemyAnimator.SetTrigger("Die");
        Destroy(gameObject, 2f);
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
