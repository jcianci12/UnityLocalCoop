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
    private float shipSpeed = 20.0f; 
    private Vector3 move;

    public void FixedUpdate()
    {

        if (move != Vector3.zero)
        {
            //gameObject.transform.parent = null;

            //parent.transform.forward = move;
            
                //var x = move.x * Time.deltaTime * 8.0f;
                //var y = move.y * Time.deltaTime * 8.0f;
                //Debug.Log("engine move!" + x + " " + y);
            //shipRigidBody.gameObject.transform.Translate(x, 0, y);
            //shipRigidBody.gameObject.transform.forward = (move * Time.deltaTime * (shipSpeed));
            shipRigidBody.gameObject.transform.Rotate(Vector3.up,move.x);
            Debug.Log("translate "+move.z * 10);
            shipRigidBody.gameObject.GetComponent<Rigidbody>().AddForce(-transform.right  * move.z*shipSpeed);
            

            //shipRigidBody.gameObject.transform.Translate(move * Time.deltaTime * shipSpeed);
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
       
            float vertical = movement.y;
            float horizontal = movement.x;

            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;
            forward.y = 0;
            right.y = 0;
            forward = forward.normalized;
            right = right.normalized;

            Vector3 forwardRelativeVerticalInput = vertical * forward;
            Vector3 rightRelativeHorizontalInput = horizontal * right;

            Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeHorizontalInput;
            Debug.Log(cameraRelativeMovement.x + " " + cameraRelativeMovement.y);
        //Debug.Log("movement"+ movement.x + " " + movement.y);

        //move = new Vector3(movement.x * right.x, 0, movement.y * forward.y);
        //move = cameraRelativeMovement;
        move = new Vector3(movement.x , 0, movement.y);



    }
    public void AttachPlayer(GameObject player)
    {
        
        var ship = GameObject.FindGameObjectWithTag("Ship");
        
        shipRigidBody = ship.GetComponent<Rigidbody>();
        
        playerRigidBody = player.GetComponent<Rigidbody>();
        
        player.transform.parent = gameObject.transform;

        var playerinputhandler = player.GetComponentInChildren<PlayerInputHandler>();
        playerinputhandler.enginepressureplate = gameObject.GetComponent<EnginePressurePlateScript>();

        //attachedPlayer = player;
    }
    public void DetachPlayer(GameObject player)
    {

        move = Vector3.zero;
        //shipRigidBody.velocity = Vector3.zero;
        //set the engine pressureplate to null
        player.GetComponentInChildren<PlayerInputHandler>().enginepressureplate = null;

        shipRigidBody = null;
        playerRigidBody = null;
        //var ship = GameObject.FindGameObjectWithTag("Ship");
        //Destroy(gameObject.GetComponent<FixedJoint>());
    }
    
}
