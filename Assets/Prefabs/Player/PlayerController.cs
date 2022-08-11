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
        controller = GetComponent<CharacterController>();
        //rb = controller.GetComponentInChildren<Rigidbody>();
        PlayerControls = new PlayerControls();

        //get the input handler


    }

    void Update()
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

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        if (MovementActive)
        {
            move = new Vector3(movement.x, 0, movement.y);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump!");
        MovementActive = true;
        controller.enabled = true;
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
            case "PressurePlate":
                DetachFromPressurePlate(other);
                break;
            case "EnginePressurePlate":
                DetachFromEnginePressurePlate(other);
                break;

        }
    }

    //private void OnEnable()
    //{
    //    //PlayerControls.Enable();
    //    jump = PlayerControls.Player.Jump;
    //    jump.Enable();
    //    jump.performed += Jump;

    //}
    //private void OnDisable()
    //{
    //    PlayerControls.Disable();
    //    jump.Disable();
    //}
    private void AttachToPressurePlate(Collider other) //the pressure plate
    {
        this.MovementActive = false;

        controller.transform.position = other.transform.position;

        move = Vector3.zero;
        
        //playerInputHandler.AttachPlayerToPressurePlate(other.GetComponent<PressurePlate>(), gameObject);
        var pp = other.GetComponent<PressurePlate>();
        pp.AttachPlayer(gameObject);
    }
    
    private void DetachFromPressurePlate(Collider other)
    {
        //playerInputHandler.DetachPlayerToPressurePlate(other.GetComponent<PressurePlate>(), gameObject);
        var pp = other.GetComponent<PressurePlate>();
        pp.DetachPlayer(gameObject);
        playerInputHandler = null;
    }
    private void AttachToEnginePressurePlate(Collider collider)
    {
        this.MovementActive = false;
        //controller.transform.position = collider.transform.position;
        move = Vector3.zero;
        var pp = collider.GetComponent<EnginePressurePlateScript>();
        pp.AttachPlayer(gameObject);
        //playerInputHandler.AttachPlayerToPressurePlate(collider.GetComponent<PressurePlate>(), gameObject);
    }
    private void DetachFromEnginePressurePlate(Collider collider)
    {

    }

}
