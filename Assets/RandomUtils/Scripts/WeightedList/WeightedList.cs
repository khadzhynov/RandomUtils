using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GG.Infrastructure.Utils
{
    [Serializable] public class WeightedListOfInts : WeightedList<int> { }
    [Serializable] public class WeightedListOfStrings : WeightedList<string> { }
    [Serializable] public class WeightedListOfVector3 : WeightedList<Vector3> { }
    [Serializable] public class WeightedListOfGameObjects : WeightedList<GameObject> { }
    [Serializable] public class WeightedListOfMonoBehaviours : WeightedList<MonoBehaviour> { }
    [Serializable] public class WeightedListOfScriptableObjects : WeightedList<ScriptableObject> { }

    public interface IReadOnlyWeightedList<T>
    {
        T GetRandomByWeight();
        float GetWeightAtIndex(int index);
        float GetTotalWeight();
        float GetNormalizedWeightAtIndex(int index);
    }

    public abstract class WeightedListBase
    {
        public abstract void Normalize();
    }

    public class WeightedList<T> : WeightedListBase, IReadOnlyWeightedList<T>, IList<T>, IReadOnlyList<T>
    {
        [SerializeField]
        private List<T> _objects;

        [SerializeField]
        private List<float> _weights;

        public WeightedList(List<T> objects, List<float> weights)
        {
            _objects = objects;
            _weights = weights;
        }

        public WeightedList()
        {
            _objects = new List<T>();
            _weights = new List<float>();
        }

        public int Count => _objects.Count;

        public bool IsReadOnly => false;

        public T this[int index]
        {
            get => _objects[index];
            set => _objects[index] = value;
        }

        public T GetRandomByWeight()
        {
            float totalWeight = 0;

            for (int i = 0; i < _weights.Count; ++i)
            {
                totalWeight += _weights[i];
            }

            float randomWeight = UnityEngine.Random.Range(0, totalWeight);

            float currentWeight = 0;

            T result = default(T);
            bool success = false;

            for (int i = 0; i < _weights.Count; ++i)
            {
                currentWeight += _weights[i];

                if (currentWeight > randomWeight)
                {
                    success = true;
                    result = _objects[i];

                    break;
                }
            }

            if (!success)
            {
                Debug.LogError("Can not find proper item, return null or default");
            }
            return result;
        }
        
        public float GetWeightAtIndex(int index)
        {
            return _weights[index];
        }

        public float GetTotalWeight()
        {
            float totalWeight = 0;
            foreach (var weight in _weights)
            {
                totalWeight += weight;
            }
            return totalWeight;
        }

        public float GetNormalizedWeightAtIndex(int index)
        {
            return _weights[index] / GetTotalWeight();
        }

        public void SetWeightAtIndex(int index, float weight)
        {
            _weights[index] = weight;
        }

        public override void Normalize()
        {
            List<float> normalizedWeights = new List<float>();

            for (int i = 0; i < _weights.Count; ++i)
            {
                normalizedWeights.Add(GetNormalizedWeightAtIndex(i));
            }

            _weights = new List<float>(normalizedWeights);
        }

        public void SetWeightOf(T item, float weight)
        {
            int index = _objects.FindIndex(x => x.Equals(item));
            SetWeightAtIndex(index, weight);
        }

        public int IndexOf(T item)
        {
            return _objects.IndexOf(item);
        }

        /// <summary>
        /// Be aware when operate with weighted list of floats!
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        public int IndexOf(float weight)
        {
            return _weights.IndexOf(weight);
        }

        public void Insert(int index, T item)
        {
            Insert(index, item, 0);
        }

        public void Insert(int index, T item, float weight)
        {
            _objects.Insert(index, item);
            _weights.Insert(index, weight);
        }

        public void RemoveAt(int index)
        {
            _objects.RemoveAt(index);
            _weights.RemoveAt(index);
        }

        public void Add(T item)
        {
            Add(item, 0f);
        }

        public void Add(T item, float weight)
        {
            _objects.Add(item);
            _weights.Add(weight);
        }

        public void Clear()
        {
            _objects.Clear();
            _weights.Clear();
        }

        public bool Contains(T item)
        {
            return _objects.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _objects.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            int index = _objects.FindIndex(x => x.Equals(item));
            if (index != -1)
            {
                _objects.Remove(item);
                _weights.RemoveAt(index);
            }

            return index != -1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _objects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _objects.GetEnumerator();
        }
    }
}
