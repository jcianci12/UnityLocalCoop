using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDock : MonoBehaviour
{


    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange;
    [SerializeField] private float pickupForce;
    [SerializeField] private float drag;

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
        //we can allow the player to jump by not restricting the y axis
        var player = parent.transform.GetComponentInChildren<PlayerController>();

        var rb = player.GetComponent<Rigidbody>();


        if (Vector3.Distance(player.transform.position, transform.position) > pickupRange)
        {

            Vector3 moveDirection = (
                new Vector3(transform.position.x, 0, transform.position.z)
                -
                                new Vector3(player.transform.position.x, 0, player.transform.position.z)

                );
            rb.MovePosition(moveDirection * pickupForce);
        }


    }
}
