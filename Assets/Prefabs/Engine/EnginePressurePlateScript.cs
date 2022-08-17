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
            
                var x = move.x * Time.deltaTime * 8.0f;
                var y = move.y * Time.deltaTime * 8.0f;
                Debug.Log("engine move!" + x + " " + y);
            //shipRigidBody.gameObject.transform.Translate(x, 0, y);
            shipRigidBody.gameObject.transform.forward = move;
            shipRigidBody.gameObject.transform.Translate(Vector3.forward * 8.0f *Time.deltaTime);
                //transform.Translate(x, 0, y);
            
                //playerRigidBody.gameObject.GetComponent<CharacterController>().transform.Translate(x,0,y);
                //shipRigidBody.AddForce(move);
                //attachedPlayer.GetComponent<Rigidbody>().AddForce(move);

            

            //gameObject.transform.SetParent(parent.transform);
            // gun.transform.forward = move;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {

        Vector2 movement = context.ReadValue<Vector2>();
        move = new Vector3(movement.x, 0, movement.y);

    }
    public void AttachPlayer(GameObject player)
    {
        
        var ship = GameObject.FindGameObjectWithTag("Ship");
        
        shipRigidBody = ship.GetComponent<Rigidbody>();
        
        playerRigidBody = player.GetComponent<Rigidbody>();
        
        player.transform.parent = gameObject.transform;

        var playerinputhandler = player.GetComponentInChildren<PlayerInputHandler>();
        playerinputhandler.epp = gameObject.GetComponent<EnginePressurePlateScript>();

        //attachedPlayer = player;
    }
    public void DetachPlayer(GameObject player)
    {

        move = Vector3.zero;
        //shipRigidBody.velocity = Vector3.zero;
        //set the engine pressureplate to null
        player.GetComponentInChildren<PlayerInputHandler>().epp = null;

        shipRigidBody = null;
        playerRigidBody = null;
        //var ship = GameObject.FindGameObjectWithTag("Ship");
        //Destroy(gameObject.GetComponent<FixedJoint>());
    }
}
