using UnityEngine;

[RequireComponent(typeof(AnimatorHandler), typeof(CircleCollider2D))]
public class Player : MonoBehaviour
{
    private SceneReloader _sceneReloader;
    private AnimatorHandler _animatorHandler;
    private CircleCollider2D _circleCollider;
    
    private int _currentHealth;
    private int _maxHealth = 3;

    private void Start()
    {
        _sceneReloader = FindObjectOfType<SceneReloader>();
        _animatorHandler = GetComponent<AnimatorHandler>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _currentHealth = _maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            Destroy(coin.gameObject);
        }
        else if (other.TryGetComponent(out FirstAidKit firstAidKit))
        {
            RecountHealth(1);
            Destroy(firstAidKit.gameObject);
        }
    }

    public void RecountHealth(int deltaHealth)
    {
        _currentHealth += deltaHealth;
        Debug.Log("_currentHealth = " + _currentHealth);

        if (deltaHealth < 0)
        {
            _animatorHandler.SetDamaged();
        }

        if (_currentHealth <= 0)
        {
            _circleCollider.enabled = false;
            Invoke(nameof(Death), 1.5f);
        }
    }

    private void Death()
    {
        _sceneReloader.Death();
    }
}