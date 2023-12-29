using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action<float> HealthIncreased;
    public event Action<float> HealthDecreased;

    private Player _player;

    private float _currentHealth;
    private float _maxHealth = 100;
    private float _firstAidKitHealthSize = 10;

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    public void GetHealthInfo(out float maxValue, out float currentValue)
    {
        maxValue = _maxHealth;
        currentValue = _currentHealth;
    }

    public void RecountHealthByFirstAidKit()
    {
        RecountHealth(_firstAidKitHealthSize);
    }

    public void RecountHealth(float deltaHealth)
    {
        _currentHealth += deltaHealth;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        // Debug.Log("_currentHealth = " + _currentHealth);

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