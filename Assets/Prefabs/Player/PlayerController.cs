using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    private GameObject plane;
    public PlayerControls PlayerControls;
    private PlayerInputHandler playerInputHandler;
    private InputAction jump;

    private CharacterController controller;
    private Rigidbody rb;

    private Vector3 playerVelocity;

    private bool groundedPlayer;

    [SerializeField]
    public GameObject controlledObject;
    [SerializeField]
    public bool MovementActive = true;

    [SerializeField]
    private float playerSpeed = 2.0f;

    private float jumpHeight = 1f;
    private float gravityValue = -99.81f;

    private Vector3 move;
    public float m_Thrust = 20f;


    private void Awake()
    {
        cam = Camera.main;
        camPivot = Camera.main;
        controller = GetComponent<CharacterController>();
        //rb = controller.GetComponentInChildren<Rigidbody>();
        PlayerControls = new PlayerControls();
        //transform.parent = GameObject.FindGameObjectWithTag("Ship").transform;

        //get the input handler
        plane = GameObject.Find("Plane");
    }

    void FixedUpdate()
    {
        if (MovementActive)
        {
            controller.enabled = true;

            //check if player is grounded
            groundedPlayer = controller.isGrounded;
            //if the player is grounded and and velocity is more than 0
            if (groundedPlayer && playerVelocity.y < 0)
            {
                //set the player velocity to zero
                playerVelocity.y = 0f;
            }
            //inherit the movement from the parent
            //move = gameObject.transform.parent.position + move;
            controller.Move((move) * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }
            //apply gravity
            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
            controller.enabled = false;

        }

    }

    public Camera camPivot;
    public Camera cam;

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        if (MovementActive)
        {
            
            move = cam.GetComponent<CameraRelativeMovement>().GetCameraRelativeMovement(movement);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump!");
        MovementActive = true;

        if (groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        switch (other.name)
        {
            case "GunPressurePlate":
                AttachToGunPressurePlate(other);
                break;

            case "EnginePressurePlate":
                AttachToEnginePressurePlate(other);
                break;
        }
    }
    public void OnTriggerExit(Collider other)
    {

        switch (other.name)
        {
            case "GunPressurePlate":
                DetachFromGunPressurePlate(other);
                break;
            case "EnginePressurePlate":
                DetachFromEnginePressurePlate(other);
                break;

        }
    }

    private void AttachToGunPressurePlate(Collider collider) //the pressure plate
    {
        var pp = collider.GetComponent<GunPressurePlate>();
        if (pp.PlayerRigidBody ==null)
        {
            pp.AttachPlayer(gameObject);
            //check there is no one on the pressure plate
            this.MovementActive = false;
            //controller.transform.parent = collider.transform;
            move = Vector3.zero;
        }
        
    }

    private void DetachFromGunPressurePlate(Collider collider)
    {
        var pp = collider.GetComponent<GunPressurePlate>();
        var playeronpressureplateinstanceid = pp.PlayerRigidBody.gameObject.GetInstanceID();
        var colliderplayerinstanceid = gameObject.GetInstanceID();
        if (playeronpressureplateinstanceid == colliderplayerinstanceid)
        {

            pp.DetachPlayer(gameObject);
            playerInputHandler = null;
            controller.enabled = true;
        }
    }
    private void AttachToEnginePressurePlate(Collider collider)
    {
        var pp = collider.GetComponent<EnginePressurePlateScript>();
        if (pp.playerRigidBody == null)
        {


            pp.AttachPlayer(gameObject);
            //check there is no one on the pressure plate
            this.MovementActive = false;
            //controller.transform.parent = collider.transform;
            move = Vector3.zero;
        }

        //playerInputHandler.AttachPlayerToPressurePlate(collider.GetComponent<PressurePlate>(), gameObject);
    }
    private void DetachFromEnginePressurePlate(Collider collider)
    {
        
        var pp = collider.GetComponent<EnginePressurePlateScript>();
        var playeronpressureplateinstanceid = pp.playerRigidBody.gameObject.GetInstanceID();
        var colliderplayerinstanceid = gameObject.GetInstanceID();
        if (playeronpressureplateinstanceid ==colliderplayerinstanceid)
        {

        pp.DetachPlayer(gameObject);
        playerInputHandler = null;
        controller.enabled = true;
        }
       // Debug.Log("player on plate:" + pp.playerRigidBody.gameObject.GetComponent<PlayerController>().GetInstanceID() + " this player " + gameObject.GetInstanceID());


    }

}
