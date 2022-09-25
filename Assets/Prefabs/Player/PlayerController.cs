using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    //test
    public bool CargoInRange;
    public float CargoPickupRange = 1f;
    public LayerMask cargoLayer;


    private void Awake()
    {
        cam = Camera.main;
        camPivot = Camera.main;
        controller = GetComponent<CharacterController>();
        //rb = controller.GetComponentInChildren<Rigidbody>();
        PlayerControls = new PlayerControls();
        //transform.parent = GameObject.FindGameObjectWithTag("Ship").transform;

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
            //apply gravity
            playerVelocity.y += gravityValue * Time.deltaTime;

            //inherit the movement from the parent
            //move = gameObject.transform.parent.position + move;
            controller.Move((move) * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }
            controller.Move(playerVelocity * Time.deltaTime);
            controller.enabled = false;

        }

    }
    public void Update()
    {
        CargoInRange = Physics.CheckSphere(transform.position, CargoPickupRange, cargoLayer);
    }

    public Camera camPivot;
    public Camera cam;
    public Transform held;

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        if (MovementActive)
        {

            var maincamera = GameObject.Find("Main Camera");
            if (maincamera)
            {
                move = maincamera.GetComponent<CameraRelativeMovement>().GetCameraRelativeMovement(movement);

            }
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
        if (pp.PlayerRigidBody == null)
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
        if (playeronpressureplateinstanceid == colliderplayerinstanceid)
        {

            pp.DetachPlayer(gameObject);
            playerInputHandler = null;
            controller.enabled = true;
        }
    }
    public void PickupCargo()
    {
        //we need to know if we are near cargo
        //check for closest cargo
        if (CargoInRange)
        {
            var cargo = GameObject.FindGameObjectsWithTag("Cargo")?.Select(i => i.transform).ToList();
            var closest = GetClosestCargo(cargo, transform);
            if (closest)
            {

                closest.gameObject.GetComponent<gravity>().Active =false;
                closest.transform.SetParent(transform);
                held = closest;
            }
        }
    }
    public void DropCargo()
    {
        if (held)
        {
            held.GetComponent<gravity>().Active = true;
            held.parent = null;
            held = null;

        }

    }
    Transform GetClosestCargo(List<Transform> cargo, Transform fromThis)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromThis.position;

        foreach (Transform potentialTarget in cargo)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }

}
