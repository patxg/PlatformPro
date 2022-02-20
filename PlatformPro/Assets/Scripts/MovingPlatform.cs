using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform _targetA, _targetB;
    [SerializeField] private float _speed = 5.0f;
    private bool _switch = false;

    void Start()
    {
    }

    void Update()
    {
        if (_switch == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetB.position, _speed * Time.deltaTime);
        }
        else if (_switch == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetA.position, _speed * Time.deltaTime);
        }
        if (transform.position == _targetB.position)
        {
            _switch = true;
        }
        else if (transform.position == _targetA.position)
        {
            _switch = false;
        }
    }
    // Take the player parent = this game object
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }

}
