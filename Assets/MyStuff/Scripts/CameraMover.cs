using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private Transform _targetTransform;

    private float _speed = 3;

    void Start()
    {
        Player player = FindObjectOfType<Player>();
        _targetTransform = player.transform;
        // вначале смотрим точно на игрока
        transform.position =
            new Vector3(_targetTransform.position.x, _targetTransform.position.y, transform.position.z);
    }

    void Update()
    {
        Vector3 targetPosition = _targetTransform.position;
        targetPosition.z = transform.position.z;
        // плавное движение камеры
        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
    }
}