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
    public TextMeshProUGUI healthDisplay;

    [Header("Weapons")]
    public List<GameObject> unlockedWeapons;
    public GameObject[] allWeapons;
    public Image weaponIcon;

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

        if (!facingRight && moveInput.x < 0)
        {
            Flip();
        }
        else if (facingRight && moveInput.x > 0)
        {
            Flip();
        }

        if (health <= 0)
        {
            // Перезагружаем сцену, когда здоровье становится 0
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon();
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
        if (other.CompareTag("Potion"))
        {
            ChangeHealth(5);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Weapon"))
        {
            for (int i = 0; i < allWeapons.Length; i++)
            {
                if (other.name == allWeapons[i].name)
                {
                    unlockedWeapons.Add(allWeapons[i]);
                }
            }
            SwitchWeapon();
            Destroy(other.gameObject);
        }
    }

    public void ChangeHealth(int healthValue)
    {
        health += healthValue;
        healthDisplay.text = "HP: " + health;
    }

    // Метод, который вызывается для нанесения урона игроку
    public void TakeDamage(int damage)
    {
        health -= damage;  // Уменьшаем здоровье на полученный урон
        healthDisplay.text = "HP: " + health;  // Обновляем отображение здоровья

        // Проверяем, если здоровье игрока меньше или равно 0, то перезагружаем сцену
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Перезагрузка сцены
        }

        Debug.Log("Player health after damage: " + health);  // Логируем здоровье игрока после получения урона
    }

    public void SwitchWeapon()
    {
        for (int i = 0; i < unlockedWeapons.Count; i++)
        {
            if (unlockedWeapons[i].activeInHierarchy)
            {
                unlockedWeapons[i].SetActive(false);
                if (i != 0)
                {
                    unlockedWeapons[i - 1].SetActive(true);
                    weaponIcon.sprite = unlockedWeapons[i - 1].GetComponent<SpriteRenderer>().sprite;
                }
                else
                {
                    unlockedWeapons[unlockedWeapons.Count - 1].SetActive(true);
                    weaponIcon.sprite = unlockedWeapons[unlockedWeapons.Count - 1].GetComponent<SpriteRenderer>().sprite;
                }
                weaponIcon.SetNativeSize();
                break;
            }
        }
    }
}
