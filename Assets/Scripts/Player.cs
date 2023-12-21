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

    // при столкновении коллайдера с врагом - получить урон 
    // анимация атаки игрока - просто цветом? 
    // анимация атаки врага - просто цветом?
    // у врага тоже есть жизни 
    // у врага 2 режима - патрулирувать / преследовать 
    // несколько врагов на карте
    // аптечки - случайно разбросаны на карте - лечат 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player OnCollisionEnter2D name = " + collision.collider.name);

        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            Debug.Log("столкновение с врагом!");
        }
    }

    // для сбора монеток
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Player OnTriggerEnter name = " + other.name);
        if (other.TryGetComponent(out Coin coin))
        {
            // Debug.Log("Player OnTriggerEnter  Coin coin ");
            Destroy(coin.gameObject);
        }
    }

    public void RecountHealth(int deltaHealth)
    {
        // tip: deltaHealth может быть отрицательная 
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
            //       но хотелось придумать более универсальное решение
            if (runningTime > durationTime && doReturnBaseColor == false)
            {
                doReturnBaseColor = true;
                (baseColor, targetColor) = (targetColor, baseColor);
                runningTime = 0;
            }

            yield return null;
        }

        yield return null;
    }

    private void Death()
    {
        _sceneReloader.Death();
    }
}