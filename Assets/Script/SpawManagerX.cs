using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRangeX = 4;
    private float spawnZMin = 3;
    private float spawnZMax = 7;

    public int enemyCount;
    public int waveCount = 1;

    // Bonus: inimigos ficam mais rápidos
    public float enemySpeed = 50.0f;
    public float enemySpeedIncrease = 5.0f;

    void Start()
    {
        SpawnEnemyWave(waveCount);
    }


    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0)
        {
            waveCount++;

            enemySpeed += enemySpeedIncrease;

            SpawnEnemyWave(waveCount);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);

            EnemyX enemyScript = enemy.GetComponent<EnemyX>();

            if (enemyScript != null)
            {
                enemyScript.speed = enemySpeed;
            }
     
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);

        return new Vector3(xPos, 1.0f, zPos);
    }

}