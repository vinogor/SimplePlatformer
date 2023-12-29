using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class PlayerHealthSliderViewChanger : MonoBehaviour
{
    [SerializeField] private bool _isSmoothly;

    private Slider _slider;
    private PlayerHealth _playerHealth;

    private void OnEnable()
    {
        _slider = GetComponent<Slider>();
        _playerHealth = GetComponentInParent<Player>().GetComponent<PlayerHealth>();
        _playerHealth.HealthIncreased += HandleChange;
        _playerHealth.HealthDecreased += HandleChange;
    }

    private void Start()
    {
        _playerHealth.GetHealthInfo(out float maxValue, out float currentValue);
        _slider.maxValue = maxValue;
        _slider.value = currentValue;
    }

    private void OnDisable()
    {
        _playerHealth.HealthIncreased -= HandleChange;
        _playerHealth.HealthDecreased -= HandleChange;
    }

    private void HandleChange(float currentValue)
    {
        if (_isSmoothly == false)
        {
            _slider.value = currentValue;
        }
        else
        {
            StartCoroutine(ChangeValue(currentValue));
        }
    }

    private IEnumerator ChangeValue(float targetValue)
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