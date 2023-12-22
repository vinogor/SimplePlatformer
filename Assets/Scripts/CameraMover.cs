using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private Transform _targetTransform;

    private float _speed = 3.5f;

    void Start()
    {
        Player player = FindObjectOfType<Player>();
        _targetTransform = player.transform;
        transform.position =
            new Vector3(_targetTransform.position.x, _targetTransform.position.y, transform.position.z);
    }

    void Update()
    {
        Vector3 targetPosition = _targetTransform.position;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
    }
}