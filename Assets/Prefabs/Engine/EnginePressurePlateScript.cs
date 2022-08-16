using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class EnginePressurePlateScript : MonoBehaviour
{
    private Rigidbody shipRigidBody;
    private Rigidbody playerRigidBody;
    // private static GameObject attachedPlayer;
    public GunInputHandler gunInputHandler;
    private Vector3 move;

    public void Update()
    {

        if (move != Vector3.zero)
        {
            //gameObject.transform.parent = null;

            //parent.transform.forward = move;
            if (shipRigidBody != null)
            {
                shipRigidBody.AddForce(move);
                //attachedPlayer.GetComponent<Rigidbody>().AddForce(move);

            }

            //gameObject.transform.SetParent(parent.transform);
            // gun.transform.forward = move;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {

        Vector2 movement = context.ReadValue<Vector2>();
        Debug.Log("engine move!" + movement.x + " " + movement.y);
        move = new Vector3(movement.x, 0, movement.y);

    }
    public void AttachPlayer(GameObject player)
    {
        //attach the player to the pressure plate
        //get the ship object
        var ship = GameObject.FindGameObjectWithTag("Ship");
        //get the rigidbodies so we can add a force
        shipRigidBody = ship.GetComponent<Rigidbody>();
        //get the player rigidbody so we can add a joint to the ship
        playerRigidBody = player.GetComponent<Rigidbody>();
        //set the player to kinematic so it isnt in the physics calcs
        //playerRigidBody.isKinematic = false;
        //connect the player to the ship

        //make the player a child of the pressureplate
        player.transform.parent = gameObject.transform;

        gameObject.AddComponent<FixedJoint>().connectedBody = playerRigidBody;

        var playerinputhandler = player.GetComponentInChildren<PlayerInputHandler>();
        playerinputhandler.epp = gameObject.GetComponent<EnginePressurePlateScript>();

        //attachedPlayer = player;
    }
    public void DetachPlayer(GameObject player)
    {
        player.transform.parent = GameObject.FindGameObjectWithTag("Ship").transform;

        move = Vector3.zero;
        shipRigidBody.velocity = Vector3.zero;
        //set the engine pressureplate to null
        player.GetComponentInChildren<PlayerInputHandler>().epp = null;

        shipRigidBody = null;
        playerRigidBody = null;
        //var ship = GameObject.FindGameObjectWithTag("Ship");
        Destroy(gameObject.GetComponent<FixedJoint>());
    }
}
