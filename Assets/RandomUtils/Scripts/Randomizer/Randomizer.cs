using System;
using System.Collections.Generic;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace GG.Infrastructure.Utils
{
    [Serializable]
    public class Randomizer
    {
        private int _amount;
        private int _lastSelected;
        private int[] _occurances;

        public int Amount { get => _amount; }

        public Randomizer(int amount)
        {
            Assert.IsTrue(amount > 0, "Randomizer can work only with amounts > 0");
            _amount = amount;
            _lastSelected = Random.Range(0, amount - 1);
            _occurances = new int[_amount];
        }

        /// <summary>
        /// Select without repeat twice the same value.
        /// Example: 
        /// input array [1, 2, 3]
        /// example output: 1, 3, 1, 2, 3, 1, 2, 1, 2, 1, 3, 1, 2, 3, 1, 3
        /// </summary>
        public int SelectNoRepeat()
        {
            if (_amount == 1)
            {
                return 0;
            }

            int newSelection = Random.Range(0, _amount);

            int attempts = 1000;

            while (newSelection == _lastSelected && attempts > 0)
            {
                attempts--;
                newSelection = Random.Range(0, _amount);
            }

            _lastSelected = newSelection;
            _occurances[newSelection]++;

            return newSelection;
        }

        /// <summary>
        /// Select with flat distribution line. Minimizes random fluctuations.
        /// /// Example: 
        /// input array [1, 2, 3]
        /// example output: 1, 3, 2, 3, 2, 1, 2, 1, 3, 3, 2, 1, 2, 3, 1, 1
        /// So here is 6 of "1", 5 of "2" and 5 of "3". 
        /// It tries to keep the same amount of occurances for each value.
        /// </summary>
        public int SelectFlatDistributed()
        {
            if (_amount == 1)
            {
                return 0;
            }

            int minOccurances = GetMinimalOccurance();

            List<int> minIndices = GetListOfMinimalOccurances(minOccurances);

            int newSelection = minIndices[Random.Range(0, minIndices.Count)];
            while (newSelection == _lastSelected)
            {
                newSelection = minIndices[Random.Range(0, minIndices.Count)];
            }

            _lastSelected = newSelection;
            _occurances[newSelection]++;

            return newSelection;
        }

        private List<int> GetListOfMinimalOccurances(int minOccurances)
        {
            List<int> minIndices = new List<int>();

            for (int i = 0; i < _occurances.Length; ++i)
            {
                if (_occurances[i] == minOccurances)
                {
                    minIndices.Add(i);
                }
            }

            return minIndices;
        }

        private int GetMinimalOccurance()
        {
            int minOccurances = int.MaxValue;

            for (int i = 0; i < _occurances.Length; ++i)
            {
                if (_occurances[i] < minOccurances)
                {
                    minOccurances = _occurances[i];
                }
            }

            return minOccurances;
        }
    }
}
