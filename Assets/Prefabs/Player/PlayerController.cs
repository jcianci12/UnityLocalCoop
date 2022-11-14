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
        rb = gameObject.GetComponent<Rigidbody>();
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

        //setparenttoground();

    }
    //void setparenttoground()
    //{
    //    var ray = new Ray(this.transform.position,-this.transform.up);
    //    Debug.DrawLine(ray.origin,ray.direction);
    //    RaycastHit RaycastHitDown;
    //    if (Physics.Raycast(ray, out RaycastHitDown, 2f))
    //    {
    //        transform.parent = RaycastHitDown.transform;
    //    }
    //    else
    //    {
    //        transform.parent = null;
    //    }
    //}
    public void Update()
    {
        if (heldObj != null)
        {
            //move object
            MoveObject();

        }
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
            case "Floor":
                transform.parent = other.transform;
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
            case "Floor":
                transform.parent = null;
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
        var pps = collider.GetComponent<GunPressurePlate>();
        var playeronpressureplateinstanceid = pps.PlayerRigidBody.gameObject.GetInstanceID();
        var colliderplayerinstanceid = gameObject.GetInstanceID();
        if (playeronpressureplateinstanceid == colliderplayerinstanceid)
        {

            pps.DetachPlayer(gameObject);
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
            this.MovementActive = false;
            move = Vector3.zero;
        }
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
    
    public void DropCargo()
    {
        if (held)
        {
            held.GetComponent<gravity>().Active = true;
            held.parent = null;
            held = null;

        }
    }
    

    [Header("PIckup Settings")]
    [SerializeField] Transform holdArea;
    private GameObject heldObj;
    private Rigidbody heldObjRB;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 1.0f;
    [SerializeField] private float pickupForce = 150f;

    public void PickupCargo()
    {
        if (heldObj == null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
            {
                //
                PickupObject(hit.transform.gameObject);
            }

        }
        else
        {
            //drop
            DropObject();
        }
    }
    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = false;
            heldObjRB.drag = 10;
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjRB.transform.parent = holdArea;
            heldObj = pickObj;
        }
    }
    public void DropObject()
    {

        heldObjRB.useGravity = true;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;

        heldObjRB.transform.parent = null;
        heldObj = null;

    }
    void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }

}
