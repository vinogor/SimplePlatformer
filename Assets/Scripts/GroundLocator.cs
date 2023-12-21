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
        float epsilonDistance = 0.05f;
        var circleCastAll = Physics2D.CircleCastAll(
            _collider.bounds.center,
            _collider.radius,
            Vector2.down,
            epsilonDistance,
            LayerMask.GetMask("Ground"));

        // Debug.Log("circleCastAll.Length = " + circleCastAll.Length);

        return circleCastAll.Length == 1;
    }
}