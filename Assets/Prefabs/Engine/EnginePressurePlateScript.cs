using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class EnginePressurePlateScript : MonoBehaviour
{
    private Rigidbody shipRigidBody;
    public Rigidbody playerRigidBody;
    // private static GameObject attachedPlayer;
    public GunInputHandler gunInputHandler;
    public float thrust = 2000.0f;
    private Vector3 move;
    public Light[] engineLight;
    public Camera cam;
    public float torque;


    public void Start()
    {
        var ship = GameObject.FindGameObjectWithTag("Ship");

        shipRigidBody = ship.GetComponent<Rigidbody>();

    }

    public void FixedUpdate()
    {

        if (move != Vector3.zero)
        {
            //shipRigidBody.gameObject.transform.Rotate(Vector3.up, move.x);
            shipRigidBody.gameObject.GetComponent<Rigidbody>().AddTorque(transform.up * move.x * torque);

            //Debug.Log("translate " + move.z * 10);

            shipRigidBody.gameObject.GetComponent<Rigidbody>().AddForce(-transform.right * move.z * thrust);
            
            //var thrust = new Vector3(0, 0, move.z);
            //shipRigidBody.gameObject.GetComponent<Rigidbody>().MovePosition(shipRigidBody.transform.position+thrust * shipSpeed *Time.deltaTime);
            //(transform.position + m_Input * Time.deltaTime * m_Speed)


            //shipRigidBody.gameObject.transform.Find("Cube").gameObject.GetComponent<Rigidbody>().AddForce(-transform.right * move.z * shipSpeed);

        }
        foreach (var light in engineLight)
        {
            if (light != null)
            {
                light.intensity = ExtensionMethods.Remap(shipRigidBody.gameObject.GetComponent<Rigidbody>().velocity.magnitude, 0, 100, 1, 100);

            }
        }
    }
   

    public void OnMove(InputAction.CallbackContext context)
    {

        Vector2 movement = context.ReadValue<Vector2>();


        float vertical = movement.y;
        float horizontal = movement.x;
        Debug.Log(vertical + " " + horizontal);


        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;
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
        move = new Vector3(movement.x, 0, movement.y);
    }


    public void AttachPlayer(GameObject player)
    {
        if (playerRigidBody == null)
        {
            playerRigidBody = player.GetComponent<Rigidbody>();
            player.transform.parent = gameObject.transform;
            var playerinputhandler = player.GetComponentInChildren<PlayerInputHandler>();
            playerinputhandler.enginepressureplate = gameObject.GetComponent<EnginePressurePlateScript>();
        }
        //attachedPlayer = player;
    }
    public void DetachPlayer(GameObject player)
    {
        move = Vector3.zero;
        //shipRigidBody.velocity = Vector3.zero;
        //set the engine pressureplate to null
        player.GetComponentInChildren<PlayerInputHandler>().enginepressureplate = null;

        playerRigidBody = null;
        //var ship = GameObject.FindGameObjectWithTag("Ship");
        //Destroy(gameObject.GetComponent<FixedJoint>());
    }

    // unclamped


}
public static class ExtensionMethods
{
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}