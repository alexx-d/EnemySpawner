using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(ColorChanger))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private ColorChanger _colorChanger;
    private Rigidbody _rigidbody;
    private Transform _target;

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

        if (Vector3.Distance(transform.position, _target.position) < 1f)
        {
            Destroy(gameObject);
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
