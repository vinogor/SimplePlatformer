using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSliderViewChanger : MonoBehaviour
{
    [SerializeField] private bool _isSmoothly;

    private PlayerHealth _playerHealth;
    private Slider _slider;

    private void OnEnable()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _slider = GetComponent<Slider>();
        _playerHealth.HealthChanged += HandleHealthChange;
    }

    private void Start()
    {
        _playerHealth.GetHealthInfo(out int maxValue, out int currentValue);
        _slider.maxValue = maxValue;
        _slider.value = currentValue;
    }

    private void OnDisable()
    {
        _playerHealth.HealthChanged -= HandleHealthChange;
    }

    private void HandleHealthChange(int maxValue, int currentValue)
    {
        _slider.maxValue = maxValue;

        if (_isSmoothly == false)
        {
            _slider.value = currentValue;
        }
        else
        {
            StartCoroutine(ChangeValue(currentValue));
        }
    }

    private IEnumerator ChangeValue(int targetValue)
    {
        float durationTime = 0.5f;
        float runningTime = 0;
        float startValue = _slider.value;

        while (durationTime - runningTime > float.Epsilon)
        {
            _slider.value = Mathf.Lerp(startValue, targetValue, runningTime / durationTime);
            runningTime += Time.deltaTime;
            yield return null;
        }

        _slider.value = targetValue;

        yield return null;
    }
}