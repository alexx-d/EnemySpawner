using System.Collections;
using UnityEngine;

public class EnemySpawnTimer : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] _spawners;
    [SerializeField] private float _delay = 2f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            yield return wait;

            if (_spawners.Length > 0)
            {
                int index = Random.Range(0, _spawners.Length);
                _spawners[index].Spawn();
            }
        }
    }
}

