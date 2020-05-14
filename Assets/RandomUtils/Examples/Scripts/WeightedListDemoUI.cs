using UnityEngine;
using UnityEngine.UI;

public class WeightedListDemoUI : MonoBehaviour
{
    private readonly string STATS = "{0}: " + System.Environment.NewLine +
        "target absolute weight: {1} " + System.Environment.NewLine +
        "target normalized weight: {2:0.00} " + System.Environment.NewLine +
        "real normalized weight: {3:0.00} " + System.Environment.NewLine +
        "absolute amount: {4}";


    [SerializeField]
    private Text _greenStats;

    [SerializeField]
    private Text _blueStats;

    [SerializeField]
    private Text _redStats;

    [SerializeField]
    private WeightedSpawner _spawner;

    private int _greenCounter;
    private int _blueCounter;
    private int _redCounter;

    private void UpdateStats()
    {
        float normalizedAmount = (float)_greenCounter / (float)_spawner.Counter;

        _greenStats.text = string.Format(STATS, 
            "Green",
            _spawner.Prefabs.GetWeightAtIndex(1),
            _spawner.Prefabs.GetNormalizedWeightAtIndex(1), 
            normalizedAmount,
            _greenCounter);

        normalizedAmount = (float)_blueCounter / (float)_spawner.Counter;

        _blueStats.text = string.Format(STATS,
            "Blue",
            _spawner.Prefabs.GetWeightAtIndex(0),
            _spawner.Prefabs.GetNormalizedWeightAtIndex(0),
            normalizedAmount,
            _blueCounter);

        normalizedAmount = (float)_redCounter / (float)_spawner.Counter;

        _redStats.text = string.Format(STATS,
            "Red",
            _spawner.Prefabs.GetWeightAtIndex(2),
            _spawner.Prefabs.GetNormalizedWeightAtIndex(2),
            normalizedAmount,
            _redCounter);

    }

    public void IncrementCounter(int prefabIndex)
    {
        switch(prefabIndex)
        {
            case 0:
                IncrementBlueCounter();
                break;

            case 1:
                IncrementGreenCounter();
                break;

            case 2:
                IncrementRedCounter();
                break;
        }
    }

    private void IncrementGreenCounter()
    {
        _greenCounter++;
        UpdateStats();
    }

    private void IncrementBlueCounter()
    {
        _blueCounter++;
        UpdateStats();
    }

    private void IncrementRedCounter()
    {
        _redCounter++;
        UpdateStats();
    }
}
