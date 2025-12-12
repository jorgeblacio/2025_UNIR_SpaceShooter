using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnX = 10f;
    
    [Header("Waves")]
    [SerializeField] private EnemyWave[] waves;
    [SerializeField] private bool autoStart = true;
    [SerializeField] private float timeBetweenWaves = 5f;
    
    private int currentWaveIndex = 0;
    private bool spawning = false;
    
    private void Start()
    {
        if (spawnPoint == null)
        {
            spawnPoint = transform;
        }
        
        if (autoStart)
        {
            StartSpawning();
        }
    }
    
    public void StartSpawning()
    {
        if (!spawning)
        {
            StartCoroutine(SpawnWaves());
        }
    }
    
    private IEnumerator SpawnWaves()
    {
        spawning = true;
        
        while (currentWaveIndex < waves.Length)
        {
            yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            currentWaveIndex++;
            
            if (currentWaveIndex < waves.Length)
            {
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        
        spawning = false;
        currentWaveIndex = 0;
        StartSpawning();
        
    }
    
    private IEnumerator SpawnWave(EnemyWave wave)
    {
        foreach (EnemyFormation formation in wave.formations)
        {
            yield return StartCoroutine(SpawnFormation(formation));
            yield return new WaitForSeconds(wave.timeBetweenFormations);
        }
    }
    
    private IEnumerator SpawnFormation(EnemyFormation formation)
    {
        foreach (Vector2 relativePos in formation.relativePositions)
        {
            Vector3 spawnPos = new Vector3(spawnX, spawnPoint.position.y + relativePos.y, 0);
            spawnPos.x += relativePos.x;
            
            Instantiate(formation.enemyPrefab, spawnPos, Quaternion.identity);
            
            yield return new WaitForSeconds(formation.spawnDelay);
        }
    }
    
    public void SpawnEnemy(GameObject enemyPrefab, Vector2 position)
    {
        Vector3 spawnPos = new Vector3(spawnX + position.x, position.y, 0);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 spawnPos = new Vector3(spawnX, transform.position.y, 0);
        Gizmos.DrawLine(spawnPos + Vector3.up * 10, spawnPos + Vector3.down * 10);
    }

    public void ResetSpawner()
    {
        StopAllCoroutines();
        currentWaveIndex = 0;
        spawning = false;
    }

    [System.Serializable]
    public class EnemyFormation
    {
        public GameObject enemyPrefab;
        public Vector2[] relativePositions; 
        public float spawnDelay = 0.2f; 
    }

    [System.Serializable]
    public class EnemyWave
    {
        public EnemyFormation[] formations;
        public float timeBetweenFormations = 3f;
    }
}