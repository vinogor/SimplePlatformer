using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public event Action<float> HealthDecreased;

    [SerializeField] float pushForce;

    private Enemy _enemy;

    private float _currentHealth;
    private float _maxHealth = 100;

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    public void TakeDamage(float damage, bool shouldPush = true)
    {
        _currentHealth += damage;
        HealthDecreased?.Invoke(_currentHealth);

        if (shouldPush)
        {
            _enemy.GetComponent<Rigidbody2D>().AddForce(transform.up * pushForce, ForceMode2D.Impulse);
        }

        if (_currentHealth <= 0)
        {
            _enemy.Die();
        }
    }

    public void GetHealthInfo(out float maxValue, out float currentValue)
    {
        maxValue = _maxHealth;
        currentValue = _currentHealth;
    }
}