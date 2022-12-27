using System.Collections.Generic;
using UnityEngine;

public class SC_RigidbodyMagnet : MonoBehaviour
{
    public float magnetForce = 100;
    //public Rigidbody ship;

    public List<GameObject> caughtRigidbodies = new List<GameObject>();

    void FixedUpdate()
    {
        for (int i = 0; i < caughtRigidbodies.Count; i++)
        {

            //rb.velocity = (transform.position - (caughtRigidbodies[i].transform.position + caughtRigidbodies[i].centerOfMass)) * magnetForce * Time.deltaTime;
            if (caughtRigidbodies[i] != null)
            {
                caughtRigidbodies[i].GetComponentInParent<Rigidbody>().AddForceAtPosition((
                               transform.position - caughtRigidbodies[i].GetComponentInChildren<shipCoupling>().transform.position) * magnetForce, transform.position);
                Debug.DrawLine(transform.position, caughtRigidbodies[i].transform.GetComponentInChildren<shipCoupling>().transform.position);

            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<shipCoupling>())
        {
            GameObject r = other.transform.gameObject;

            if (!caughtRigidbodies.Contains(r))
            {
                //Add Rigidbody
                caughtRigidbodies.Add(r);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<shipCoupling>())
        {
            GameObject r = other.transform.gameObject;

            if (caughtRigidbodies.Contains(r))
            {
                //Remove Rigidbody
                caughtRigidbodies.Remove(r);
            }
        }
    }
}