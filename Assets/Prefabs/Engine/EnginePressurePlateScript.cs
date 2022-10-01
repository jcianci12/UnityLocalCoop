using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class EnginePressurePlateScript : MonoBehaviour
{
    private Rigidbody shipRigidBody;
    // private static GameObject attachedPlayer;
    public GunInputHandler gunInputHandler;
    public float thrust = 2000.0f;
    private Vector3 move;
    public Light[] engineLight;
    public Camera cam;
    public float torque;
    [Header("PIckup Settings")]
    [SerializeField] Transform holdArea;
    private GameObject heldObj;
    public Rigidbody playerRigidBody;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 1.0f;
    [SerializeField] private float pickupForce = 500f;
    [SerializeField] private float drag = 5;



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
        if (playerRigidBody != null)
        {
            MoveObject();
        }
        foreach (var light in engineLight)
        {
            if (light != null)
            {
                light.intensity = ExtensionMethods.Remap(shipRigidBody.gameObject.GetComponent<Rigidbody>().velocity.magnitude, 0, 100, 1, 100);

            }
        }
    }
    void MoveObject()
    {

        Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
        //playerRigidBody.AddForce((moveDirection * 2) * pickupForce);

        playerRigidBody.MovePosition(holdArea.position);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();

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
            heldObj = player;
            playerRigidBody = heldObj.GetComponent<Rigidbody>();
            //heldObjRB.mass = 10;
            //playerRigidBody.isKinematic = true;
        }
    }
    public void DetachPlayer(GameObject player)
    {
        move = Vector3.zero;

        player.GetComponentInChildren<PlayerInputHandler>().enginepressureplate = null;

        playerRigidBody = null;
        heldObj = null;
        //playerRigidBody.isKinematic = false;
        playerRigidBody = null;


    }



}
public static class ExtensionMethods
{
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}