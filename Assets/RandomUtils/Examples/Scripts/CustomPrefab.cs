using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPrefab : MonoBehaviour
{
    [SerializeField]
    private Vector3 _direction;

    private Rigidbody _rigidbody;

    private float _speed;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    void Update()
    {
        _rigidbody.velocity = _direction.normalized * _speed;
    }
}
