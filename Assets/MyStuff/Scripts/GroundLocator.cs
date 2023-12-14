using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GroundLocator : MonoBehaviour
{
    private CircleCollider2D _collider;

    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    public bool IsStandOnGround()
    {
        float distance = 0.01f;
        var circleCastAll = Physics2D.CircleCastAll(_collider.bounds.center, _collider.radius, Vector2.down, distance,
            LayerMask.GetMask("Ground"));
        return circleCastAll.Length == 1;
    }
}