using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class EnginePressurePlateScript : MonoBehaviour
{
    private Rigidbody shipRigidBody;
    private float distToGround;

    // private static GameObject attachedPlayer;
    public float thrust = 2000.0f;
    private Vector3 move;
    public Light[] engineLight;
    public float torque;
    GameObject player;



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

            Debug.Log("adding thrust " + move.z * thrust);

            shipRigidBody.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * move.z * thrust);

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

        move = new Vector3(movement.x, 0, movement.y);
    }

    
    public void AttachPlayerToEnginePressurePlate(GameObject go)
    {
        
        if (transform.GetComponentsInChildren<PlayerController>().Length == 0)
        {
            //make the player a child
            player = go;
            player.transform.parent = transform;
            player.GetComponent<PlayerController>().MovementActive = false;

        }
    }
    public void DetachPlayerFromEnginePressurePlate(GameObject go)
    {
        //check if the pressure plate has this player attached
        if (go.transform.IsChildOf(transform))
        {
            player.transform.parent = null;
            player.GetComponent<PlayerController>().MovementActive = true;
            move = Vector3.zero;
            
            player.transform.parent = shipRigidBody.transform;
            player = null;
        };

    }



}
public static class ExtensionMethods
{
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}