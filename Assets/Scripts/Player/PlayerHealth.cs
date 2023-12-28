using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action<int> HealthIncreased;
    public event Action<int> HealthDecreased;

    private Player _player;

    private int _currentHealth;
    private int _maxHealth = 100;
    private int _firstAidKitHealthSize = 10;

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    public void GetHealthInfo(out int maxValue, out int currentValue)
    {
        maxValue = _maxHealth;
        currentValue = _currentHealth;
    }

    public void RecountHealthByFirstAidKit()
    {
        RecountHealth(_firstAidKitHealthSize);
    }

    public void RecountHealth(int deltaHealth)
    {
        _currentHealth += deltaHealth;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        Debug.Log("_currentHealth = " + _currentHealth);

        if (deltaHealth < 0)
        {
            HealthDecreased?.Invoke(_currentHealth);
        }
        else
        {
            HealthIncreased?.Invoke(_currentHealth);
        }

        if (_currentHealth <= 0)
        {
            _player.Die();
        }
    }
}