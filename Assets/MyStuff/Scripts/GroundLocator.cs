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
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(LayerMask.NameToLayer("Ground"));
        contactFilter.useLayerMask = true;
        
        RaycastHit2D[] raycastHits = new RaycastHit2D[10];

        float distance = 0.01f;

        Vector2 center = _collider.bounds.center;
        int circleCast = Physics2D.CircleCast(
            center,
            _collider.radius,
            Vector2.down,
            contactFilter,
            raycastHits,
            distance);
        
        Debug.Log("IsStandOnGround circleCast = " + circleCast);
        return circleCast == 1;
    }
}