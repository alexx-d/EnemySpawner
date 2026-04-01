using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : ObjectPool<Enemy>
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private float _repeatRate = 2f;

    private void Start()
    {
        StartCoroutine(SpawnWithDelay());
    }

    public Enemy GetRandomly()
    {
        Enemy enemy = Get();

        if (enemy != null)
        {
            SpawnPoint randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

            enemy.transform.SetPositionAndRotation(
                randomSpawnPoint.transform.position,
                randomSpawnPoint.transform.rotation
            );

            Vector3 randomDirection = GetRandomDirection();
            enemy.Init(randomDirection);

            enemy.Died += OnEnemyDied;
        }

        return enemy;
    }

    private IEnumerator SpawnWithDelay()
    {
        var wait = new WaitForSeconds(_repeatRate);

        while (enabled)
        {
            GetRandomly();
            yield return wait;
        }
    }

    private Vector3 GetRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        return Quaternion.Euler(0, randomAngle, 0) * Vector3.forward;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        enemy.Died -= OnEnemyDied;

        Release(enemy);
    }
}