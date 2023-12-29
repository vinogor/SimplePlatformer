using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMaskForEnemySearch;

    private SpriteRenderer _enemySearchCircleRenderer;
    private Player _player;
    private TMP_Text _text;

    private float _searchRadius = 2.5f;
    private float _damage = 0.1f;
    private float _durationTime = 6f;

    private void Start()
    {
        _enemySearchCircleRenderer = GetComponentInChildren<EnemySearchCircle>().GetComponent<SpriteRenderer>();
        _player = GetComponent<Player>();
        _text = GetComponentInChildren<TMP_Text>();
        _text.enabled = false;
    }

    public void SearchEnemy()
    {
        StartCoroutine(VampireHealth());
    }

    private IEnumerator VampireHealth()
    {
        _enemySearchCircleRenderer.enabled = true;
        _text.enabled = true;
        float timeLeft = _durationTime;

        while (timeLeft > float.Epsilon)
        {
            if (TryFindEnemy(out Enemy enemy))
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                PlayerHealth playerHealth = _player.GetComponent<PlayerHealth>();

                enemyHealth.TakeDamage(-_damage, false);
                playerHealth.RecountHealth(_damage);

                // Debug.Log("_timeLeft = " + timeLeft);
                UpdateTimeText(timeLeft);
            }
            else
            {
                _enemySearchCircleRenderer.enabled = false;
                _text.enabled = false;
                yield break;
            }

            timeLeft -= Time.deltaTime;
            yield return null;
        }

        _enemySearchCircleRenderer.enabled = false;
        _text.enabled = false;
        yield return null;
    }

    private bool TryFindEnemy(out Enemy enemy)
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _searchRadius, _layerMaskForEnemySearch);

        if (collider != null && collider.TryGetComponent(out Enemy enemyForReturn))
        {
            enemy = enemyForReturn;
            return true;
        }

        enemy = null;
        return false;
    }

    private void UpdateTimeText(float timeLeft)
    {
        _text.text = Math.Ceiling(timeLeft).ToString();
    }
}