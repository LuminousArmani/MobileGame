using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    public int baseEnemies = 8;
    public float enemiesPerSecond = 0.5f;
    public float timeBetweenWaves = 5f;
    public float difficultyScalingFactor = 0.75f;
    public float enemiesPerSecondCap = 20f;
    [SerializeField] private TextMeshProUGUI waveNumberText;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps; // enemies per second
    private bool isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        SetDifficulty();
        StartCoroutine(StartWave());
        UpdateWaveText();
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void SetDifficulty()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        switch (currentScene.name)
        {
            case "EarthEasy":
                difficultyScalingFactor = 0.60f;
                enemiesPerSecondCap = 15f;
                timeBetweenWaves = 6f;
                baseEnemies = 8;
                break;

            case "MarsMedium":
                difficultyScalingFactor = 0.75f;
                enemiesPerSecondCap = 20f;
                timeBetweenWaves = 4f;
                baseEnemies = 12;
                break;

            case "NeptuneHard":
                difficultyScalingFactor = 0.95f;
                enemiesPerSecondCap = 30f;
                timeBetweenWaves = 1f;
                baseEnemies = 15;
                break;

            default:
                Debug.LogWarning("Scene not recognized. Using default difficulty settings.");
                break;
        }
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        UpdateWaveText();
        StartCoroutine(StartWave());

        // Check if the player has reached the victory condition
        if ((currentWave == 20 && difficultyScalingFactor == 0.60f) ||
            (currentWave == 35 && difficultyScalingFactor == 0.75f) ||
            (currentWave == 50 && difficultyScalingFactor == 0.95f) ||
            currentWave == 90)
        {
            SceneManager.LoadScene("Win");
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private void UpdateWaveText()
    {
        if (waveNumberText != null)
        {
            waveNumberText.text = $"Wave: {currentWave}";
        }
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }

    private void SpawnEnemy()
    {
        int index = DetermineEnemyIndex();
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, TDLevelManager.main.startPoint.position, Quaternion.identity);
    }

    private int DetermineEnemyIndex()
    {
        if (currentWave < 3)
            return 0;
        if (currentWave < 7)
            return Random.Range(0, 3);
        if (currentWave >= 12)
        {
            int[] allowedIndices = { 0, 2, 3, 4, 5 };
            return allowedIndices[Random.Range(0, allowedIndices.Length)];
        }
        if (currentWave < 28)
            return Random.Range(4, 8);
        if (currentWave < 32)
            return Random.Range(7, 13);
        if (currentWave < 48)
            return Random.Range(11, 15);

        return 0; // Fallback
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0, enemiesPerSecondCap);
    }
}
