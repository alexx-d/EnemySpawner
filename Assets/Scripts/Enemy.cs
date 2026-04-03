using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(ColorChanger))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private ColorChanger _colorChanger;
    private Rigidbody _rigidbody;
    private Transform _target;
    private readonly float _distanceToTarget = 1f;

    public event Action<Enemy> Died;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_target == null)
        {
            return;
        }

        Vector3 direction = (_target.position - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }

        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

        if (transform.position.IsEnoughClose(_target.position, _distanceToTarget))
        {
            Died?.Invoke(this);
        }
    }

    public void Init(Transform target)
    {
        _colorChanger.ApplyRandomColor();

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _target = target;
    }
}
