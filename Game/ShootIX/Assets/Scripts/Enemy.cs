using UnityEngine;
using TMPro;
using System;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> OnEnemyKilled;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float health;
    [SerializeField] private GameObject _death;
    [SerializeField] private float moveSpeed = 2f;

    public Color EnemyColor { get; set; }

    private TextMeshPro healthText; // Text display for health


    private void Awake()
    {
        // Get the SpriteRenderer component on the enemy (assumes it's attached to the same GameObject)
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        
        
        // Create a new TextMeshPro object to display health
        GameObject textObj = new GameObject("HealthText");
        textObj.transform.SetParent(transform);
        textObj.transform.localPosition = Vector3.zero + new Vector3(0, 0.5f, 0); // Position slightly above the enemy

        healthText = textObj.AddComponent<TextMeshPro>();
        healthText.fontSize = 1;
        healthText.alignment = TextAlignmentOptions.Center;
        healthText.text = health.ToString();
    }

    private void Update()
    {
        // Move the enemy down
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

    public void Initialize(float newHealth, Color newColor)
    {
        health = newHealth;
        EnemyColor = newColor;
        UpdateHealthText();

      

        // Apply the color to the sprite renderer
        if (spriteRenderer != null)
        {
            
            spriteRenderer.color = newColor;
            Debug.Log($"Setting enemy color to: {spriteRenderer.color}");

        }
    }

    public void UpdateHealth(float change)
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
