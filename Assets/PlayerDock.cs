using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDock : MonoBehaviour
{
    

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange ;
    [SerializeField] private float pickupForce;
    [SerializeField] private float drag ;

    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (parent.transform.GetComponentInChildren<PlayerController>() != null)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        var player = parent.transform.GetComponentInChildren<PlayerController>();
        //Vector3 moveDirection = (holdArea.position - player.transform.position);
        //playerRigidBody.AddForce((moveDirection * 2) * pickupForce);

        var rb = player.GetComponent<Rigidbody>();
        
        //rb.useGravity = false;
        //rb.AddForce(holdArea.position);
        if (Vector3.Distance(player.transform.position, transform.position) > pickupRange)
        {
            Vector3 moveDirection = (transform.position - player.transform.position);
            rb.AddForce(moveDirection * pickupForce);
        }
        //player.GetComponent<Rigidbody>().MoveRotation(holdArea.rotation);


    }
}
