using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    // Detect Moving Box
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "MovingBox")
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            //Debug.Log("Distance" + distance);
            // When close to center
            if (distance < 0.05f)
            {
                Rigidbody box = other.GetComponent<Rigidbody>();
                if (box != null)
                {
                    //disable the box's rigid body or set it to kinematic (no longer can move)
                    box.isKinematic = true;
                }
                MeshRenderer boxRenderer = GetComponentInChildren<MeshRenderer>();
                if (boxRenderer != null)
                {
                    //change pressure pad to green
                    boxRenderer.material.color = Color.green;
                }
                Destroy(this);
            }
        }
    }
}
