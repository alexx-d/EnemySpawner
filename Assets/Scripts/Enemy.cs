using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(ColorChanger))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _dieYLevel = -10f;

    private ColorChanger _colorChanger;
    private Rigidbody _rigidbody;
    private Vector3 _direction;
    private float _speedSqr;

    public event Action<Enemy> Died;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _rigidbody = GetComponent<Rigidbody>();

        _speedSqr = _speed * _speed;
    }

    private void FixedUpdate()
    {
        if (_direction == Vector3.zero)
        {
            return;
        }

        Vector3 horizontalVelocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);

        if (horizontalVelocity.sqrMagnitude < _speedSqr)
        {
            _rigidbody.AddForce(_direction * _speed, ForceMode.Acceleration);
        }
    }

    private void Update()
    {
        if (transform.position.y < _dieYLevel)
        {
            Died?.Invoke(this);
        }
    }

    public void Init(Vector3 direction)
    {
        _colorChanger.ApplyRandomColor();

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _direction = direction.normalized;

        if (_direction != Vector3.zero)
        {
            transform.forward = _direction;
        }
    }
}
