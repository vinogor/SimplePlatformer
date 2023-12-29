using UnityEngine;

[RequireComponent(typeof(AnimatorHandler), typeof(CircleCollider2D))]
public class Player : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private SceneReloader _sceneReloader;
    private CircleCollider2D _circleCollider;

    private void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _sceneReloader = FindObjectOfType<SceneReloader>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            Destroy(coin.gameObject);
        }
        else if (other.TryGetComponent(out FirstAidKit firstAidKit))
        {
            _playerHealth.RecountHealthByFirstAidKit();
            Destroy(firstAidKit.gameObject);
        }
    }

    public void Die()
    {
        _circleCollider.enabled = false;
        Invoke(nameof(Death), 1.5f);
    }

    private void Death()
    {
        _sceneReloader.Death();
    }
}