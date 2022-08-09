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

    private void Jump(InputAction.CallbackContext context)
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
        if (other.name == "PresurePlate")
        {
            AttachToPressurePlate(other);
                       

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.name == "PresurePlate")
        {
            DetachFromPressurePlate(other);

        }
    }

    private void OnEnable()
    {
        //PlayerControls.Enable();
        jump = PlayerControls.Player.Jump;
        jump.Enable();
        jump.performed += Jump;

    }
    private void OnDisable()
    {
        PlayerControls.Disable();
        jump.Disable();
    }
    private void AttachToPressurePlate(Collider other) //the pressure plate
    {
        this.MovementActive = false;

        controller.transform.position = other.transform.position;
        
        move = Vector3.zero;

        //PressurePlate.AttachPlayer(controller);


        //pressureplate = other.GetComponent<PressurePlate>();
        //var parent = pressureplate.transform.parent.gameObject;
        //var guninpuhandler = parent.GetComponent<GunInputHandler>();
        //var charactercontroller = other.GetComponent<CharacterController>();
        //var b = parent;
        //
        //pressureplate.AttachPlayer(gameObject.GetComponent<CharacterController>(),parent,guninpuhandler);
        var go = gameObject;
        var pi = gameObject.GetComponentInChildren<PlayerInput>();
        playerInputHandler = pi.GetComponent<PlayerInputHandler>();
        playerInputHandler.AttachPlayerToPressurePlate(other.GetComponent<PressurePlate>(),gameObject);

        //var playerinput = go.GetComponentInChildren<PlayerInput>();
        //var b = pih;

        //need to get the playerinputhandler



    }
    private void DetachFromPressurePlate(Collider other)
    {
        //PressurePlate.DetachPlayer(controller);
        playerInputHandler.DetachPlayerToPressurePlate(other.GetComponent<PressurePlate>(), gameObject);
        playerInputHandler = null;
       // other.GetComponent<PressurePlate>().DetachPlayer(gameObject);
        
    }


}
