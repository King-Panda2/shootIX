using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Color BulletColor { get; set; } // Color of the bullet
    private bool isShooting = false;

    [SerializeField] private float speed;
    private Vector3 direction;

    void Update()
    {
        // Check for movement
        if (isShooting)
        {
            transform.position += (direction * speed * Time.deltaTime);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is an enemy
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null && enemy.EnemyColor == BulletColor)
        {
            // If the bullet color matches the enemy color, destroy the bullet
            Destroy(gameObject);
        }
    }*/

    void OnBecameInvisible()
    {
        // Self-destruct when out of range
        Destroy(gameObject);
    }

    public void Shoot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - transform.position).normalized;
        direction = new Vector3(direction.x, direction.y, 0.0f);
        
        isShooting = true;
    }
}

