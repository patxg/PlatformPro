using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField] private MeshRenderer _callButton;
    [SerializeField] private int _requiredCoins = 4;
    private Elevator _elevator;
    private bool _elevatorCalled = false;

    private void Start()
    {
        _elevator = GameObject.Find("Elevator").GetComponent<Elevator>();
        if (_elevator == null)
        {
            Debug.LogError("The Elevator is NULL");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyUp(KeyCode.E) && other.GetComponent<Player>().CoinCount() >= _requiredCoins)
            {
                if (_elevatorCalled == true)
                {
                    _callButton.material.color = Color.red;
                }
                else
                {
                    _callButton.material.color = Color.green;
                    _elevatorCalled = true;
                }
                _elevator.CallElevator();
            }
        }
    }
}
