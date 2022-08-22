using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{


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
        var ship = GameObject.FindGameObjectWithTag("Ship");
        controller.transform.parent = ship.transform;


        //get the input handler
    }




    void Update()
    {
        if (MovementActive)
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
            controller.Move(move * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }
            //apply gravity
            playerVelocity.y += gravityValue * Time.deltaTime;

            controller.Move(playerVelocity * Time.deltaTime);
        }
        //move player relative to ship
        controller.Move(playerVelocity * Time.deltaTime);
        
    }

    public Camera camPivot;
    public Camera cam;

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        if (MovementActive)
        {
            float vertical = movement.y;
            float horizontal = movement.x;

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
            //move = new Vector3(movement.x * right.x, 0, movement.y * forward.y);
            move = cameraRelativeMovement;
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
                AttachToPressurePlate(other);
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
                DetachFromPressurePlate(other);
                break;
            case "EnginePressurePlate":
                DetachFromEnginePressurePlate(other);
                break;

        }
    }

    private void AttachToPressurePlate(Collider other) //the pressure plate
    {
        this.MovementActive = false;

        controller.transform.parent = other.transform;

        move = Vector3.zero;

        var pp = other.GetComponent<PressurePlate>();
        pp.AttachPlayer(gameObject);
    }

    private void DetachFromPressurePlate(Collider other)
    {
        var pp = other.GetComponent<PressurePlate>();
        pp.DetachPlayer(gameObject);
        playerInputHandler = null;
        MovementActive = true;
        controller.enabled = true;
    }
    private void AttachToEnginePressurePlate(Collider collider)
    {
        this.MovementActive = false;
        //controller.transform.parent = collider.transform;
        move = Vector3.zero;

        var pp = collider.GetComponent<EnginePressurePlateScript>();
        pp.AttachPlayer(gameObject);
        //playerInputHandler.AttachPlayerToPressurePlate(collider.GetComponent<PressurePlate>(), gameObject);
    }
    private void DetachFromEnginePressurePlate(Collider collider)
    {
        var pp = collider.GetComponent<EnginePressurePlateScript>();
        pp.DetachPlayer(gameObject);
        playerInputHandler = null;
        controller.enabled = true;
    }

}
