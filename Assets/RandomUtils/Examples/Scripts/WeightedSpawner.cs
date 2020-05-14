using GG.Infrastructure.Utils;
using System;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


//-------------------- To see your Weighted List of custom items in inspector, you need to do next:

//------ 1: Inherit concrete implementation of generic WeightedList (don`t forget the Serializable attribute):
[Serializable]
public class WeightedListOfPrefabs : WeightedList<CustomPrefab> { }

//------ 2: Inherit property drawer and mark it with attribute 
//------ (or you can just add attribute to WeightedListPropertyDrawer class itself): 
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(WeightedListOfPrefabs))]
public class MyPropertyDrawer : WeightedListPropertyDrawer { }
#endif
//------ Thats all! Now you can setup your WeightedListOfPrefabs in inspector!


public class WeightedSpawner : MonoBehaviour
{
    [SerializeField]
    private WeightedListOfPrefabs _prefabsList = new WeightedListOfPrefabs();

    [SerializeField]
    private float _speed = 1f;

    [SerializeField]
    private int _counter = 0;

    public IReadOnlyWeightedList<CustomPrefab> Prefabs { get => _prefabsList; }

    public int Counter { get => _counter; }

    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(1 / _speed);
            CustomPrefab prefab = _prefabsList.GetRandomByWeight();
            CustomPrefab newInstance = Instantiate(prefab);
            newInstance.SetSpeed(_speed);
            _counter++;
        }
    }
}
