using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float pushForce;

    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            player.RecountHealth(-10);

            // отскок при ударе - в сторону движения врага
            Vector2 direction = _renderer.flipX ? transform.right : transform.right * -1;
            player.GetComponent<Rigidbody2D>().AddForce(direction * pushForce, ForceMode2D.Impulse);
        }
    }
}