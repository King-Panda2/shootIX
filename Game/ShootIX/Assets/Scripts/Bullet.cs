using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Color BulletColor { get; set; } // Color of the bullet
    private bool isShooting = false;

    void Update()
    {
        if (isShooting)
        {
            // TO DO: move
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is an enemy
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null && enemy.EnemyColor == BulletColor)
        {
            // If the bullet color matches the enemy color, destroy the bullet
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        isShooting = true;
    }
}

