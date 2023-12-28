using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WayPointMovement : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _pursuitDistance = 3f;

    private SpriteRenderer _renderer;
    private Transform[] _points;
    private int _currentPoint;
    private float _speed = 2;
    private bool _shouldPursue;

    private void Start()
    {
        _shouldPursue = false;
        _renderer = GetComponent<SpriteRenderer>();
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        Vector3 target;

        if (TryLookForPlayer(out Vector3 playerPosition))
        {
            target = playerPosition;
            _shouldPursue = true;
        }
        else
        {
            target = _points[_currentPoint].position;

            if (_shouldPursue)
            {
                _shouldPursue = false;
                TakeNextPoint();
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
        TurnAround(target);
    }

    private bool TryLookForPlayer(out Vector3 playerPosition)
    {
        Vector2 direction = _renderer.flipX ? Vector2.right : Vector2.left;
        RaycastHit2D[] hits = new RaycastHit2D[2];
        var size = Physics2D.RaycastNonAlloc(transform.position, direction, hits, _pursuitDistance);

        if (size == 1)
        {
            playerPosition = default;
            return false;
        }

        RaycastHit2D hit = hits[1];

        if (hit.collider != null && hit.collider.TryGetComponent(out Player player))
        {
            playerPosition = player.transform.position;
            return true;
        }

        playerPosition = default;
        return false;
    }

    private void TurnAround(Vector3 target)
    {
        if (transform.position == target)
        {
            TakeNextPoint();
        }
    }

    private void TakeNextPoint()
    {
        _currentPoint++;
        _renderer.flipX = !_renderer.flipX;

        if (_currentPoint >= _points.Length)
        {
            _currentPoint = 0;
        }
    }
}