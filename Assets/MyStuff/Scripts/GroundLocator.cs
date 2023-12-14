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
        Debug.Log("IsStandOnGround");

        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.layerMask = LayerMask.NameToLayer("Ground");

        RaycastHit2D[] raycastHits = new RaycastHit2D[10];

        float distance = 0.1f;

        Vector2 center = _collider.bounds.center;
        int circleCast = Physics2D.CircleCast(
            center,
            _collider.radius,
            Vector2.down,
            contactFilter,
            raycastHits,
            distance);
        
        Debug.DrawLine(center, new Vector2(center.x, center.y - _collider.radius - distance), Color.red);

        // TODO: а это норм что он столкновение с самим собой учитывает?

        Debug.Log("IsStandOnGround circleCast = " + circleCast);
        return circleCast > 1;
    }
}