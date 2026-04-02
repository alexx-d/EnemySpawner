using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 5f;

    private int _currentWaypointIndex = 0;

    private void Update()
    {
        if (_waypoints.Length == 0)
        {  
            return; 
        }

        Transform targetPoint = _waypoints[_currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, _speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
        }
    }

    public void SetWaypoints(Transform[] waypoints)
    {
        _waypoints = waypoints;
    }
}
