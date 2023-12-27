using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthChanger : MonoBehaviour
{
    public event Action<int> HealthChanging;

    [SerializeField] private int _deltaHealth;

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ChangeHealth);
    }

    private void ChangeHealth()
    {
        HealthChanging?.Invoke(_deltaHealth);
    }
}