using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public event Action<int> HealthDecreased;
    [SerializeField] float pushForce;

    private Enemy _enemy;

    private int _currentHealth;
    private int _maxHealth = 100;

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth += damage;
        
        Debug.Log("ENEMY _currentHealth = " + _currentHealth);
        
        HealthDecreased?.Invoke(_currentHealth);
        
        _enemy.GetComponent<Rigidbody2D>().AddForce(transform.up * pushForce, ForceMode2D.Impulse);

        if (_currentHealth <= 0)
        {
            _enemy.Die();
        }
    }

    public void GetHealthInfo(out int maxValue, out int currentValue)
    {
        maxValue = _maxHealth;
        currentValue = _currentHealth;
    }
}