using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public int health;
    public float speed;

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
