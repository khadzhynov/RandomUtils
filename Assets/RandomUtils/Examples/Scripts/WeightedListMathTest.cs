using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GG.Infrastructure.Utils;


// Items to operate with:
public enum ItemsWithWeights
{
    Item1 = 1,
    Item2 = 10,
    Item3 = 20,
    Item4 = 100
}

public class WeightedListMathTest : MonoBehaviour
{
    private WeightedList<ItemsWithWeights> _items = new WeightedList<ItemsWithWeights>();

    private int _attempts = 1000;

    [SerializeField]
    private Text _output;

    [SerializeField]
    private Button _testButton;

    void Start()
    {
        _testButton.onClick.AddListener(Test);

        Test();
    }

    private void Test()
    {
        _output.text = string.Empty;

        // Fill weighted list with items and weights:
        _items.Add(ItemsWithWeights.Item1, (float)ItemsWithWeights.Item1);
        _items.Add(ItemsWithWeights.Item2, (float)ItemsWithWeights.Item2);
        _items.Add(ItemsWithWeights.Item3, (float)ItemsWithWeights.Item3);
        _items.Add(ItemsWithWeights.Item4, (float)ItemsWithWeights.Item4);

        //Prepare container for results:
        Dictionary<ItemsWithWeights, int> results = new Dictionary<ItemsWithWeights, int>();

        results.Add(ItemsWithWeights.Item1, 0);
        results.Add(ItemsWithWeights.Item2, 0);
        results.Add(ItemsWithWeights.Item3, 0);
        results.Add(ItemsWithWeights.Item4, 0);

        // Begin test:
        ItemsWithWeights currentResult;
        for (int i = 0; i < _attempts; i++)
        {
            // Get item
            currentResult = _items.GetRandomByWeight();

            // Save result
            results[currentResult]++;
        }

        // Log results:
        string message = "Weighted list test. " + _attempts + " attempts: ";
        Debug.Log(message);
        _output.text += message + System.Environment.NewLine;

        foreach (var key in results.Keys)
        {
            message = "Item " + key.ToString() + " with weight " + ((float)key) + " occurs " + results[key] + " times";
            _output.text += message + System.Environment.NewLine;
            Debug.Log(message);
        }
    }


    /*
     * Interpretation:
     * 
     * Absolute weights amount is 1 + 10 + 20 + 100 = 131
     * 
     * Weight 1.31 has 1% chance to appear
     * 
     * Item 1 has weight 1 and 0.76% chance to appear;
     * Item 2 has weight 10 and 7.63% chance to appear; 
     * Item 3 has weight 20 and 15.27% chance to appear; 
     * Item 4 has weight 100 and 76.33% chance to appear; 
     * 
     * Check: 0.76 + 7.63 + 15.27 + 76.33 = 99.99 (0.01 is rounding inaccuracy)
     * 
     * For 1000 attempts:                   Actual results from 10 tests:
     * Item 1 should occur 7.6 times        |8      |7      |8      |6      |5      |10     |8      |5      |8      |5
     * Item 2 should occur 76.3 times       |60     |73     |64     |72     |91     |71     |90     |80     |70     |79
     * Item 3 should occur 152.7 times      |151    |152    |158    |152    |137    |162    |153    |168    |143    |141
     * Item 3 should occur 763.3 times      |781    |767    |770    |770    |767    |757    |749    |747    |779    |775
     * 
     * Average from 1000 x 10:                                                                             Mid:   Max:
     * Item 1: (8 + 7 + 8 + 6 + 5 + 10 + 8 + 5 + 8 + 5) / 10 = 7                            Inaccuracy     0.6    2.6
     * Item 2: (60 + 73 + 64 + 72 + 91 + 71 + 90 + 80 + 70 + 79) / 10 = 75                  Inaccuracy     1.3    16.3 
     * Item 3: (151 + 152 + 158 + 152 + 137 + 162 + 153 + 168 + 143 + 141) / 10 = 151.7     Inaccuracy     1      15.7
     * Item 4: (781 + 767 + 770 + 770 + 767 + 757 + 749 + 747 + 779 + 775) / 10 = 764.4     Inaccuracy     1.1    17.7
     */
}
