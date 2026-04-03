using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 5f;

    private readonly float _distanceToWaypoint = 0.1f;

    private int _currentWaypointIndex = 0;

    private void Update()
    {
        if (_waypoints.Length == 0)
        {  
            return; 
        }

        Transform targetPoint = _waypoints[_currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, _speed * Time.deltaTime);
        
        if (transform.position.IsEnoughClose(targetPoint.position, _distanceToWaypoint))
        {
            _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Length;
        }
    }
}
