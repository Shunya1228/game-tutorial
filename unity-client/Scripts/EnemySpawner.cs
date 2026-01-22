using UnityEngine;

/// <summary>
/// 敵を定期的に生成するクラス
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [Header("生成設定")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 1.5f;
    [SerializeField] private float spawnRangeX = 2.5f;
    [SerializeField] private float spawnY = 6f;

    private float nextSpawnTime;
    private bool isSpawning = true;

    void Update()
    {
        if (!isSpawning) return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    /// <summary>
    /// 敵を生成する
    /// </summary>
    private void SpawnEnemy()
    {
        if (enemyPrefab == null) return;

        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    /// <summary>
    /// 敵の生成を開始する
    /// </summary>
    public void StartSpawning()
    {
        isSpawning = true;
        nextSpawnTime = Time.time + spawnInterval;
    }

    /// <summary>
    /// 敵の生成を停止する
    /// </summary>
    public void StopSpawning()
    {
        isSpawning = false;
    }
}
