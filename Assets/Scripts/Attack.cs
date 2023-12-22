using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float _hitDistance = 1.5f;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Hit()
    {
        Vector2 direction = _spriteRenderer.flipX ? Vector2.left : Vector2.right;

        RaycastHit2D[] hits = new RaycastHit2D[2];
        var size = Physics2D.RaycastNonAlloc(transform.position, direction, hits, _hitDistance);

        if (size == 1)
        {
            return;
        }

        RaycastHit2D hit = hits[1];

        if (hit.collider.TryGetComponent(out Enemy enemy))
        {
            // Debug.Log("enemy hit!");
            enemy.Die();
        }
    }
}