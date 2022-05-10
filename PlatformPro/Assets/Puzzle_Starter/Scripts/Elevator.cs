using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private bool _goingDown = false;
    [SerializeField] private Transform _orgin, _target;
    private float _speed = 1.3f;

public void CallElevator()
    {
        _goingDown = true;
    }
    private void FixedUpdate()
    // movement of elevator when true || else
    {
        if (_goingDown == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }
        
    }
}
