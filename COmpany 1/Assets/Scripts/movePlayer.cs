using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class movePlayer : MonoBehaviour
{
    [Header("Move")]
    public float speed;

    [Header("Health")]
    public int health;
    public TextMeshPro healthDisplay;

    [Header("Shield")]
    public GameObject shield;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    private bool facingRight;

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        if(!facingRight && moveInput.x < 0)
        {
            Flip();
        }
        else if(facingRight && moveInput.x > 0)
        {
            Flip();
        }

        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Potion"))
        {
            ChangeHealth(5);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Shield"))
        {
            shield.SetActive(true);
            Destroy(other.gameObject);
        }
    }

    public void ChangeHealth(int healthValue)
    {
        health += healthValue;
        //healthDisplay.text = "HP: " + health;
    }
}
