using GG.Infrastructure.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRepeatSpawner : MonoBehaviour
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
        Vector3 spawnPosition = transform.position;
        spawnPosition.y = 0;
        while (true)
        {
            yield return new WaitForSeconds(1 / _speed);
            CustomPrefab prefab = _prefabsList[_randomizer.SelectNoRepeat()];
            CustomPrefab newInstance = Instantiate(prefab, spawnPosition, Quaternion.identity);
            newInstance.SetSpeed(_speed);
            _counter++;
        }
    }
}
