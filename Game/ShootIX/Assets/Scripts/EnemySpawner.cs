using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Prefab for the enemy
    [SerializeField] private float minimumSpawnTime; // Minimum time between spawns
    [SerializeField] private float maximumSpawnTime; // Maximum time between spawns
    [SerializeField] private int minHealth = 3;
    [SerializeField] private int maxHealth = 10;
    private float spawnTime; // Time until the next spawn
    public List<GameObject> enemyQueue = new List<GameObject>();
    public Transform[] spawnPoints; // Array of spawn points

    // Define the allowed colors for enemies
    private Color[] allowedColors = new Color[]
    {
        new Color(0.5f, 0f, 1f), // Purple (RGB: 0.5, 0, 1)
        Color.red,
        Color.blue,
        new Color(1f, 0.5f, 0f), // Orange (RGB: 1, 0.5, 0)
        Color.green,
        new Color(1f, 0.92f, 0.016f) // Yellow (RGB: (1, 0.92, 0.016)
    };

    public void GenerateEnemyList(int totalEnemies)
    {
        enemyQueue.Clear();

        for (int i = 0; i < totalEnemies; i++)
        {
            // Choose a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instantiate the enemy at the spawn point
            GameObject enemyObj = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation); 
            enemyObj.SetActive(false); // Hide until spawned

            Enemy enemy = enemyObj.GetComponent<Enemy>();
            if (enemy != null)
            {
                int health = Random.Range(minHealth, maxHealth);
                Color enemyColor = allowedColors[Random.Range(0, allowedColors.Length)];
                enemy.Initialize(health, enemyColor);
            }

            enemyQueue.Add(enemyObj);
        }
    }

    public IEnumerator SpawnEnemies()
    {
        foreach (GameObject enemy in enemyQueue)
        {
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            enemy.transform.position = randomSpawnPoint.position;
            enemy.SetActive(true);

            float randomDelay = Random.Range(minimumSpawnTime, maximumSpawnTime);
            yield return new WaitForSeconds(randomDelay);
        }
    }
}
