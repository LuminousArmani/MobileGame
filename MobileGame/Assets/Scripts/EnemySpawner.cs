using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemySpawner : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] public int baseEnemies = 8;
    [SerializeField] public float enemiesPerSecond = 0.5f;
    [SerializeField] public float timeBetweenWaves = 5f;
    [SerializeField] public float difficultyScalingFactor = 0.75f;
    [SerializeField] public float enemiesPerSecondCap = 20f;
    [SerializeField] private TextMeshProUGUI waveNumberText;

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


        Scene currentScene = SceneManager.GetActiveScene();

        // Check the scene name or index
        if (currentScene.name == "EarthEasy") 
        {
            easy = true;
        }
        else
        {
            easy = false;
        }
        if (easy == true)
        {
            difficultyScalingFactor = 0.60f;
            enemiesPerSecondCap = 15f;
            timeBetweenWaves = 6f;
            baseEnemies = 8;
        }


        if (currentScene.name == "MarsMedium") 
        {
            medium = true;
        }
        else
        {
            medium = false;
        }
        if (medium == true)
        {
            difficultyScalingFactor = 0.75f;
            enemiesPerSecondCap = 20f;
            timeBetweenWaves = 4f;
            baseEnemies = 12;
        }


        if (currentScene.name == "NeptuneHard") 
        {
            hard = true;
        }
        else
        {
            hard = false;
        }
        if (hard == true)
        {
            difficultyScalingFactor = 0.95f;
            enemiesPerSecondCap = 30f;
            timeBetweenWaves = 1f;
            baseEnemies = 15;
        }

    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        UpdateWaveText();
        StartCoroutine(StartWave());

        //difficulty wave maximum
        if (currentWave == 20 && easy == true && medium == false && hard == false || currentWave == 35 && easy == false && medium == true && hard == false || currentWave == 50 && easy == false && medium == false && hard == true || currentWave == 90)
        {
            SceneManager.LoadScene("Win");
            easy = false;
            medium = false;
            hard = false;
        }
        //if any more then one difficulty is true reset
        if (easy == true && medium == true || easy == true && hard == true || medium == true && hard == true)
        {
            SceneManager.LoadScene("TitleScreen");
            easy = false;
            medium = false;
            hard = false;
        }
    }



    private void EnemyDestroyed()//lowers the number of enemies alive
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
        yield return new WaitForSeconds(timeBetweenWaves);//gives time to set up after a wave

        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();//how fast enemies spawn
    }
    private void SpawnEnemy()
    {
        int index;

        if (currentWave < 3)
        {
            
            index = 0;
        }
        else if (currentWave < 7)
        {
            
            index = Random.Range(0, 3); // 0 or 1
        }
        else if (currentWave >= 12)
        {
            
            int[] allowedIndices = { 0, 2, 3, 4, 5 };
            index = allowedIndices[Random.Range(0, allowedIndices.Length)];
        }
        else if (currentWave < 28)
        {

            index = Random.Range(4, 8); // 0 or 1
        }
        else if (currentWave < 32)
        {

            index = Random.Range(7, 13); // 0 or 1
        }
        else if (currentWave < 48)
        {

            index = Random.Range(11, 15); // 0 or 1
        }
        else
        {
            // Fallback in case of invalid wave (shouldn't happen)
            index = 0;
        }

        // Instantiate the selected enemy prefab
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
