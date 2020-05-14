using GG.Infrastructure.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatSpawner : MonoBehaviour
{
    [SerializeField]
    private List<CustomPrefab> _prefabsList;

    [SerializeField]
    private float _speed = 1f;

    [SerializeField]
    private int _counter = 0;

    private Randomizer _randomizer;

    public IReadOnlyList<CustomPrefab> Prefabs { get => _prefabsList; }

    public int Counter { get => _counter; }

    void Start()
    {
        _randomizer = new Randomizer(_prefabsList.Count);
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / _speed);
            CustomPrefab prefab = _prefabsList[_randomizer.SelectFlatDistributed()];
            CustomPrefab newInstance = Instantiate(prefab);
            newInstance.SetSpeed(_speed);
            _counter++;
        }
    }
}
