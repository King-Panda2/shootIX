using UnityEngine;
using TMPro;
using System;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> OnEnemyKilled;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private int health;
    [SerializeField] private GameObject _death;
    [SerializeField] private float moveSpeed = 2f;

    public Color EnemyColor { get; set; }

    [SerializeField] private TextMeshPro healthText; // Text display for health

    private void Awake()
    {
        // Get the SpriteRenderer component on the enemy (assumes it's attached to the same GameObject)
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    
    private void Update()
    {
        // Move the enemy down
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

    public void Initialize(int newHealth, Color newColor)
    {
        health = newHealth;
        EnemyColor = newColor;

        // Apply the color to the sprite renderer
        if (spriteRenderer != null)
        {
            spriteRenderer.color = newColor;
        }

        UpdateHealthText();
    }

    public void UpdateHealth(int change)
    {
        health += change;
        UpdateHealthText();

        if (health <= 0)
        {
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);
        }
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
            healthText.text = health.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && bullet.GetIsShooting())
        {
            if (bullet.BulletColor == EnemyColor)
            {
                UpdateHealth(-health); // Instantly destroy if bullet matches color
            }
            else
            {
                UpdateHealth(-1); // Take 1 damage from a different-colored bullet
            }

            bullet.CollideAndDestroy();
        }
    }
}
