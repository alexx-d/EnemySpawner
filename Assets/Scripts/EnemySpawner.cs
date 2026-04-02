using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private float _spawnDelay = 2f;

    private void Start()
    {
        StartCoroutine(SpawnWithDelay());
    }

    private IEnumerator SpawnWithDelay()
    {
        var wait = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        if (_spawnPoints.Count == 0)
        {
            return;
        }
        
        SpawnPoint randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

        Enemy enemy = Instantiate(randomSpawnPoint.Prefab);

        enemy.transform.position = randomSpawnPoint.transform.position;
        enemy.Init(randomSpawnPoint.Target);
    }
}