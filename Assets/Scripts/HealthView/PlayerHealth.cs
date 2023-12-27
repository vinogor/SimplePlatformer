using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int _currentHealth;
    private int _maxHealth = 100;

    public event Action<int, int> HealthChanged;

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    public void RecountHealth(int deltaHealth)
    {
        Debug.Log("deltaHealth = " + deltaHealth);

        _currentHealth += deltaHealth;
        Debug.Log("currentHealth = " + _currentHealth);

        HealthChanged?.Invoke(_maxHealth, _currentHealth);
    }

    public void GetHealthInfo(out int maxValue, out int currentValue)
    {
        maxValue = _maxHealth;
        currentValue = _currentHealth;
    }
}