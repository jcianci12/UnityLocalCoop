using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipMap : MonoBehaviour
{
    GameObject heldObj;
    Transform holdArea;
    Rigidbody playerRigidBody;


    private void Update()
    {
        if (heldObj)
        {
            MoveObject();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //attach the player
        //heldObj = other.gameObject;

    }
    void MoveObject()
    {

        Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
        //playerRigidBody.AddForce((moveDirection * 2) * pickupForce);

        playerRigidBody.MovePosition(holdArea.position);

    }

}
    


