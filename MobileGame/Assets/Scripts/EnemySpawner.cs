using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 20f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    public bool easy = false;
    public bool medium = false;
    public bool hard = false;

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
        StartCoroutine(StartWave());
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


        Scene currentScene = SceneManager.GetActiveScene();

        // Check the scene name or index
        if (currentScene.name == "EarthEasy") // Replace "TargetSceneName" with your scene's name
        {
            easy = true;
        }
        else
        {
            easy = false;
        }


        if (currentScene.name == "MarsMedium") // Replace "TargetSceneName" with your scene's name
        {
            medium = true;
        }
        else
        {
            medium = false;
        }


        if (currentScene.name == "NeptuneHard") // Replace "TargetSceneName" with your scene's name
        {
            hard = true;
        }
        else
        {
            hard = false;
        }
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
        if (currentWave == 20 && easy == true && medium == false && hard == false || currentWave == 35 && easy == false && medium == true && hard == false || currentWave == 50 && easy == false && medium == false && hard == true || currentWave == 90)
        {
            SceneManager.LoadScene("Win");
        }
    }



    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }
    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, TDLevelManager.main.startPoint.position, Quaternion.identity);
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
