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
    private Rigidbody _rb;
    private Vector3 _direction;

    public event Action<Enemy> Died;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_direction == Vector3.zero) return;

        float currentVerticalVelocity = _rb.velocity.y;
        Vector3 horizontalMove = _direction * _speed;
        _rb.velocity = new Vector3(horizontalMove.x, currentVerticalVelocity, horizontalMove.z);
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
        _colorChanger.SetRandomColor();

        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

        _direction = direction.normalized;

        if (_direction != Vector3.zero)
        {
            transform.forward = _direction;
        }
    }
}
