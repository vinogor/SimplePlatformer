using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private SceneReloader _sceneReloader;
    private int _currentHealth;
    private int _maxHealth = 3;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _sceneReloader = FindObjectOfType<SceneReloader>();
        _currentHealth = _maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            Destroy(coin.gameObject);
        }
        else if (other.TryGetComponent(out FirstAidKit firstAidKit))
        {
            RecountHealth(1);
            Destroy(firstAidKit.gameObject);
        }
    }

    public void RecountHealth(int deltaHealth)
    {
        // deltaHealth может быть отрицательная 
        _currentHealth += deltaHealth;
        print("_currentHealth = " + _currentHealth);

        if (deltaHealth < 0)
        {
            StartCoroutine(OnHit());
        }

        if (_currentHealth <= 0)
        {
            // при смерти - падает вниз
            CircleCollider2D collider = GetComponent<CircleCollider2D>();
            collider.enabled = false;
            Invoke(nameof(Death), 1.5f);
        }
    }

    private IEnumerator OnHit()
    {
        float totalDurationTime = 1;
        float durationTime = totalDurationTime / 2;
        float runningTime = 0;
        bool doReturnBaseColor = false;

        Color baseColor = Color.white;
        Color targetColor = Color.red;

        while (runningTime <= durationTime)
        {
            _spriteRenderer.color = Color.Lerp(baseColor, targetColor, runningTime / durationTime);
            runningTime += Time.deltaTime;

            // TODO: пока хз как по красивее сделать возвращение исходного цвета
            //       можно управлять только одним каналом цвета и тогда это будет число а не Color, с ним проще,
            //       но хотелось придумать более универсальное решение для возврата цвета на исх значение
            if (runningTime > durationTime && doReturnBaseColor == false)
            {
                doReturnBaseColor = true;
                (baseColor, targetColor) = (targetColor, baseColor);
                runningTime = 0;
            }

            yield return null;
        }

        _spriteRenderer.color = targetColor;

        yield return null;
    }

    private void Death()
    {
        _sceneReloader.Death();
    }
}