using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] float pushForce;

    private Rigidbody2D _rigidbody;
    private CircleCollider2D _collider;

    private int _damageToPlayer = -10;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.RecountHealth(_damageToPlayer);
            player.GetComponent<Rigidbody2D>().AddForce(transform.up * pushForce, ForceMode2D.Impulse);
        }
    }

    public void Die()
    {
        _rigidbody.AddForce(transform.up * pushForce, ForceMode2D.Impulse);
        _collider.enabled = false;
        Invoke(nameof(Destroy), 1.5f);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}