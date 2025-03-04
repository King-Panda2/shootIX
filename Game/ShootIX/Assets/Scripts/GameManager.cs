using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton for easy access
    
    private int currentRound = 1; // Current round number
    [SerializeField] private int enemiesPerRound = 5; // Base number of enemies per round
    private int enemiesRemaining; // Number of enemies left in the current round
    [SerializeField] private float timeBetweenRounds = 1f;

    private EnemySpawner enemySpawner;
    [SerializeField] private TextMeshProUGUI enemiesLeftText;
    [SerializeField] private TextMeshProUGUI roundCounterText;

    void Awake()
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

    void Start()
    {
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
        StartRound(); // Start the first round
    }

    void Update()
    {
        // reset button
        if (Input.GetKey(KeyCode.R))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }

        // quit button
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnEnable()
    {
        Enemy.OnEnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= OnEnemyKilled;
    }

    // Start a new round
    public void StartRound()
    {
        // Calculate the number of enemies for this round
        int enemyCount = Mathf.CeilToInt(enemiesPerRound * currentRound);
        enemiesRemaining = enemyCount;

        // Spawn enemies
        enemySpawner.SpawnEnemies(enemyCount);

        // Update UI or other systems
        UpdateEnemiesLeftText();
        UpdateRoundText();
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
           StartCoroutine(EndRound());
        }
    }

    // End the current round and start the next one
    private IEnumerator EndRound()
    {
        currentRound++; // Increment the round number

        yield return new WaitForSeconds(timeBetweenRounds); // Wait between rounds

        StartRound();
    }

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
