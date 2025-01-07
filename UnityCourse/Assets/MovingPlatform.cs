using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform _minPoint;
    [SerializeField] private Transform _maxPoint;
    [SerializeField] private float _speed = 2f;

    private Vector2 _minPos, _maxPos;
    private Rigidbody2D _rb;
    private int _moveDir = 1;

    private void Start()
    {
        _minPos = _minPoint.position;
        _maxPos = _maxPoint.position;
        _rb = GetComponent<Rigidbody2D>();
        
        _rb.linearVelocity = new Vector2(_speed * _moveDir, 0);
    }

    private void FixedUpdate()
    {
        ReachingMixMaxPoints();
    }

    private void ReachingMixMaxPoints()
    {
        if (transform.position.x <= _minPos.x || transform.position.x >= _maxPos.x)
        {
            _moveDir *= -1;
            _rb.linearVelocity = new Vector2(_speed * _moveDir, 0);
        }
    }
}