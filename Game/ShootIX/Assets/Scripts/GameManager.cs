using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton for easy access
    public int currentRound = 1; // Current round number
    public int enemiesPerRound = 5; // Base number of enemies per round
    public int enemiesRemaining; // Number of enemies left in the current round

    private EnemySpawner enemySpawner;
    [SerializeField] private TextMeshProUGUI enemiesLeftText;
    [SerializeField] private TextMeshProUGUI roundCounterText;

    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _winSound;
    [SerializeField] private GameObject _bkrdMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set the singleton instance
            UpdateEnemiesLeftText();
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    private void Start()
    {
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
        StartRound(); // Start the first round
    }

    // Start a new round
    public void StartRound()
    {
        //turn off the shop UI
        // Calculate the number of enemies for this round
        int enemyCount = Mathf.CeilToInt(enemiesPerRound * currentRound );
        enemiesRemaining = enemyCount;

        // Spawn enemies
        enemySpawner.SpawnEnemies(enemyCount);

        // Update UI or other systems
        UpdateEnemiesLeftText();
        UpdateRoundText();
    }

    private void OnEnable()
    {
        Enemy.OnEnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= OnEnemyKilled;
    }

    // Called when an enemy is defeated
    public void OnEnemyKilled(Enemy enemy)
    {
        enemiesRemaining--;

        // Update UI or other systems
        UpdateEnemiesLeftText();

        // Check if all enemies are defeated
        if (enemiesRemaining <= 0)
        {
           // EndRound();
        }
    }

    // End the current round and start the next one
    /*
    private void EndRound()
    {
        if (currentRound >= 6.0f)
        {
            Instantiate(_winScreen, _centerPoint.transform.position, _centerPoint.transform.rotation);
            Instantiate(_winSound, _centerPoint.transform.position, _centerPoint.transform.rotation);
            Destroy(_bkrdMusic);
        }
        else
        {
            shop.SetActive(true);
            Debug.Log("Round " + currentRound + " completed!");
            currentRound++; // Increment the round number
        }
    }
    */

    private void UpdateEnemiesLeftText()
    {
        enemiesLeftText.text = $"Enemies Left: {enemiesRemaining}";
    }
    
    private void UpdateRoundText()
    {
        roundCounterText.text = $"Round {currentRound}";
    }
}
