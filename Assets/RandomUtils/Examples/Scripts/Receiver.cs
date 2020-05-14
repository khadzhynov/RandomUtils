using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Receiver : MonoBehaviour
{
    [SerializeField]
    private Text _counterText;

    private int _counter;

    [SerializeField]
    private WeightedListDemoUI _ui;

    [SerializeField]
    private int _prefabIndex;


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.collider.gameObject);
        _counter++;
        _counterText.text = _counter.ToString();

        if (_ui != null)
        {
            _ui.IncrementCounter(_prefabIndex);
        }
    }
}
