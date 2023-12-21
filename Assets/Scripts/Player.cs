using UnityEngine;

public class Player : MonoBehaviour
{
    private int _currentHealth;
    private int _maxHealth = 100;

    private void Start()
    {
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

        if (_currentHealth <= 0)
        {
            print("произошла смерть");
        }
    }
}