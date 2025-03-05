using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
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

    private AudioSource audioSource;
    [SerializeField] private AudioClip sfxNextRound;
    [SerializeField] private Transform enemyPreviewContainer;
    private List<GameObject> enemyPreviews = new List<GameObject>();


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
        audioSource = GetComponent<AudioSource>();
        enemySpawner = FindAnyObjectByType<EnemySpawner>();

        PrepareNextRound(); // Generate enemy preview
    }

    void Update()
    {
        // Reset button
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Quit button
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

    private void UpdateEnemiesLeftText()
    {
        enemiesLeftText.text = $"Enemies Left: {enemiesRemaining}";
    }

    private void UpdateRoundText()
    {
        roundCounterText.text = $"Round {currentRound}";
    }

    // **Prepare the round but do not start it yet**
    private void PrepareNextRound()
    {
        int enemyCount = Mathf.CeilToInt(enemiesPerRound * currentRound);
        enemySpawner.GenerateEnemyList(enemyCount); // Generate list of enemies

        DisplayEnemyPreviews(enemySpawner.enemyQueue); // Show previews in UI

        StartRound();
    }

    // **Show the generated enemies in UI preview**
    private void DisplayEnemyPreviews(List<GameObject> enemies)
    {
        foreach (GameObject enemy in enemyPreviews)
        {
            Destroy(enemy); // Clear old previews
        }
        enemyPreviews.Clear();

        foreach (GameObject enemy in enemies)
        {
            GameObject preview = Instantiate(enemy, enemyPreviewContainer);
            preview.SetActive(true); // Show in preview
            enemyPreviews.Add(preview);
        }
    }

    // Start the round
    public void StartRound()
    {
        ClearEnemyPreviews(); // Remove previewed enemies

        enemiesRemaining = enemySpawner.enemyQueue.Count;
        UpdateEnemiesLeftText();
        UpdateRoundText();

        StartCoroutine(enemySpawner.SpawnEnemies()); // Start spawning
    }

    private void ClearEnemyPreviews()
    {
        foreach (GameObject preview in enemyPreviews)
        {
            Destroy(preview);
        }
        enemyPreviews.Clear();
    }

    // **Called when an enemy is defeated**
    public void OnEnemyKilled(Enemy enemy)
    {
        enemiesRemaining--;
        UpdateEnemiesLeftText();

        if (enemiesRemaining <= 0)
        {
            StartCoroutine(EndRound());
        }
    }

    // **End the round and prepare for the next one**
    private IEnumerator EndRound()
    {
        yield return new WaitForSeconds(timeBetweenRounds);
        currentRound++;
        PlaySound(sfxNextRound);
        PrepareNextRound(); // Generate preview for next round
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
