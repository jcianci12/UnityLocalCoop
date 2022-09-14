using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMap : MonoBehaviour
{
    public float sightRange;
    public bool playerInRange;
    public LayerMask people;
    //Check for sight and attack range
    private void Update()
    {
            playerInRange = Physics.CheckSphere(transform.position, sightRange,people);

    }
}
