using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSliderViewChanger : MonoBehaviour
{
    [SerializeField] private bool _isSmoothly;

    private Slider _slider;
    private EnemyHealth _enemyHealth;

    private void OnEnable()
    {
        _slider = GetComponent<Slider>();
        _enemyHealth = GetComponentInParent<Enemy>().GetComponent<EnemyHealth>();
        _enemyHealth.HealthDecreased += HandleChange;
    }

    private void Start()
    {
        _enemyHealth.GetHealthInfo(out int maxValue, out int currentValue);
        _slider.maxValue = maxValue;
        _slider.value = currentValue;
    }

    private void OnDisable()
    {
        _enemyHealth.HealthDecreased -= HandleChange;
    }

    private void HandleChange(int currentValue)
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