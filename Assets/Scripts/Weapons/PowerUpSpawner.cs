using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("Spawning Settings")]
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField] private float minX = -8f;
    [SerializeField] private float maxX = 8f;
    [SerializeField] private float minY = -4f;
    [SerializeField] private float maxY = 4f;

    private float nextSpawnTime;

    private void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnPowerUp();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnPowerUp()
    {
        if (powerUpPrefab == null)
        {
            Debug.LogError("PowerUp Prefab is not assigned to the spawner.");
            return;
        }
        
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

        Instantiate(powerUpPrefab, randomPosition, Quaternion.identity);
    }
}