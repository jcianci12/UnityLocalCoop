using System.Collections.Generic;
using UnityEngine;

public class SC_RigidbodyMagnet : MonoBehaviour
{
    public float magnetForce = 100;
    public GameObject ShipGameObject;

    List<Rigidbody> caughtRigidbodies = new List<Rigidbody>();

    void FixedUpdate()
    {
        for (int i = 0; i < caughtRigidbodies.Count; i++)
        {
            var rb = caughtRigidbodies[i];
            rb.velocity = (transform.position - (caughtRigidbodies[i].transform.position + caughtRigidbodies[i].centerOfMass)) * magnetForce * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other==ShipGameObject)
        {
            Rigidbody r = other.transform.parent.GetComponent<Rigidbody>();

            if (!caughtRigidbodies.Contains(r))
            {
                //Add Rigidbody
                caughtRigidbodies.Add(r);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other==ShipGameObject)
        {
            Rigidbody r = other.transform.parent.GetComponent<Rigidbody>();

            if (caughtRigidbodies.Contains(r))
            {
                //Remove Rigidbody
                caughtRigidbodies.Remove(r);
            }
        }
    }
}