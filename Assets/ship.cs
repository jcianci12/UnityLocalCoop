using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ship : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
        rb.inertiaTensorRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY;
    }
    private void FixedUpdate()
    {
        
    }
}
