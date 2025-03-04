using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Prefab for the enemy
    [SerializeField] private float minimumSpawnTime; // Minimum time between spawns
    [SerializeField] private float maximumSpawnTime; // Maximum time between spawns
    private float spawnTime; // Time until the next spawn
    public Transform[] spawnPoints; // Array of spawn points

    // Define the allowed colors for enemies
    private Color[] allowedColors = new Color[]
    {
        new Color(0.5f, 0f, 1f), // Purple (RGB: 0.5, 0, 1)
        Color.red,
        Color.blue,
        new Color(1f, 0.5f, 0f), // Orange (RGB: 1, 0.5, 0)
        Color.green,
        Color.yellow
    };

    private Dictionary<Transform, Color> spawnPointColors = new Dictionary<Transform, Color>(); // Map spawn points to specific colors

    void Awake()
    {
        if (spawnPoints.Length != allowedColors.Length)
        {
            Debug.LogError("Number of spawn points must match number of colors!");
            return;
        }

        AssignColorsToSpawnPoints(); // Assign colors once at the start
        SetSpawnTime(); // Initialize the spawn time
    }

    // Assigns each spawn point a unique color
    private void AssignColorsToSpawnPoints()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPointColors[spawnPoints[i]] = allowedColors[i];
        }
    }

    // Spawn enemies over time
    public void SpawnEnemies(int count)
    {
        StartCoroutine(SpawnEnemiesOverTime(count)); // Start the coroutine
    }

    // Coroutine to spawn enemies at intervals
    private IEnumerator SpawnEnemiesOverTime(int count)
    {
        while (count > 0)
        {
            yield return new WaitForSeconds(spawnTime); // Wait for the spawn time

            // Choose a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instantiate the enemy at the spawn point
            GameObject enemyObj = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // Assign the spawn point's color to the enemy
            if (enemyObj.TryGetComponent(out Enemy enemy))
            {
                Color spawnColor = spawnPointColors[spawnPoint]; // Get assigned color
                enemy.GetComponent<SpriteRenderer>().color = spawnColor;
                enemy.EnemyColor = spawnColor; // Set the enemy's color property
            }

            count--; // Decrease the remaining enemy count
            SetSpawnTime(); // Set a new random spawn time for the next enemy
        }
    }

    // Set a random spawn time between minimum and maximum
    private void SetSpawnTime()
    {
        spawnTime = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }
}
