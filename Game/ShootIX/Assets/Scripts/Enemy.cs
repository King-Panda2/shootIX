using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> OnEnemyKilled;
    [SerializeField] private float health;
    [SerializeField] private GameObject _death;
    [SerializeField] private float moveSpeed = 2f; // Speed at which the enemy moves to the left

    public Color EnemyColor { get; set; } // Public property to access the enemy's color

    private void Update()
    {
        // Move the enemy to the left
        //transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        // Move the enemy down
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

    public void UpdateHealth(float change)
    {
        health += change;

        if (health <= 0)
        {
            //Instantiate(_death, transform.position, transform.rotation);
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is a bullet
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (bullet.GetIsShooting())
            {
                if (bullet.BulletColor == EnemyColor)
                {
                    // If the bullet color matches the enemy color, destroy the enemy
                    UpdateHealth(-health); // Reduce health to 0
                }

                // Destroy the bullet
                bullet.CollideAndDestroy();
            }
        }
    }
}