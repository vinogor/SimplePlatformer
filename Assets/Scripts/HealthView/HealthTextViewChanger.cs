using TMPro;
using UnityEngine;

public class HealthTextViewChanger : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private TMP_Text _text;

    private void OnEnable()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _text = GetComponent<TMP_Text>();
        _playerHealth.HealthChanged += HandleHealthChange;
    }

    private void Start()
    {
        _playerHealth.GetHealthInfo(out int maxValue, out int currentValue);
        _text.text = maxValue + " / " + currentValue;
    }

    private void OnDisable()
    {
        _playerHealth.HealthChanged -= HandleHealthChange;
    }

    private void HandleHealthChange(int maxValue, int currentValue)
    {
        _text.text = currentValue + "/" + maxValue;
    }
}