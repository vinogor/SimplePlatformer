using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Transform _coinPool;
    [SerializeField] private Coin _template;

    private Transform[] _coinsLocations;

    private void Start()
    {
        _coinsLocations = new Transform[_coinPool.childCount];

        for (int i = 0; i < _coinPool.childCount; i++)
        {
            _coinsLocations[i] = _coinPool.GetChild(i);
        }

        foreach (Transform coinLocation in _coinsLocations)
        {
            Instantiate(_template, coinLocation.position, Quaternion.identity);
        }
    }
}