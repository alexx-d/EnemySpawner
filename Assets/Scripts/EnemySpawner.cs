using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    [SerializeField] private Transform _target;

    public void Spawn()
    {
        Enemy enemy = Get();
        enemy.transform.position = transform.position;
        enemy.Init(_target);

        enemy.Died += OnEnemyDied;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        enemy.Died -= OnEnemyDied;
        Release(enemy);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}