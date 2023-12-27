using System.Collections.Generic;
using UnityEngine;

public class HealthChangeHandler : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private List<HealthChanger> _changers;

    private void OnEnable()
    {
        _changers = new List<HealthChanger>();

        HealthChanger[] healthChangers = FindObjectsByType<HealthChanger>(FindObjectsSortMode.None);

        foreach (HealthChanger changer in healthChangers)
        {
            _changers.Add(changer);
        }

        _changers.ForEach(changer => changer.HealthChanging += HandleHealthChange);
    }

    private void Start()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnDisable()
    {
        _changers.ForEach(changer => changer.HealthChanging -= HandleHealthChange);
    }

    private void HandleHealthChange(int delta)
    {
        _playerHealth.RecountHealth(delta);
    }
}