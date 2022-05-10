using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField]private MeshRenderer _callButton;
    //detect player
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                _callButton.material.color = Color.green;

            }
        }
    }
}
