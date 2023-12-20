using UnityEngine;

public class Player : MonoBehaviour
{
    private int _health;

    private void Start()
    {
        _health = 100;
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
        // if (collision.collider.CompareTag("Coin"))
        // {
        //     Destroy(collision.gameObject);
        // }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Player OnTriggerEnter name = " + other.name);
        if (other.TryGetComponent(out Coin coin))
        {
            // Debug.Log("Player OnTriggerEnter  Coin coin ");
            Destroy(other.gameObject);
        }
    }
}