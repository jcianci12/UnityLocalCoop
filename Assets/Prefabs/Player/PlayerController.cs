using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public PlayerControls PlayerControls;
    private PlayerInputHandler playerInputHandler;
    private InputAction jump;


    private Vector3 playerVelocity;

    private bool groundedPlayer;

    [SerializeField]
    public GameObject controlledObject;
    [SerializeField]
    public bool MovementActive = true;

    [SerializeField]
    private float playerSpeed;

    public float jumpHeight = 1f;
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


    }
    private void Start()
    {
        maincamera = GameObject.Find("Main Camera");

    }

    void FixedUpdate()
    {

        
            onShip();
        //controller.enabled = true;

        //check if player is grounded
        //if the player is grounded and and velocity is more than 0
        //if (groundedPlayer && playerVelocity.y < 0)
        //{
        //    //set the player velocity to zero
        //    playerVelocity.y = 0f;
        //}
        ////apply gravity
        //playerVelocity.y += gravityValue * Time.deltaTime;

        //inherit the movement from the parent
        //move = gameObject.transform.parent.position + move;
        //controller.Move((move) * Time.deltaTime * playerSpeed);
            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            rb.AddForce(move * playerSpeed);

        }
        //controller.Move(playerVelocity * Time.deltaTime);
        //controller.enabled = false;




    }

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
        

        var gun = transform.parent?.GetComponent<GunPressurePlate>();
        gun?.OnMove(context);
        var engine = transform.parent?.GetComponent<EnginePressurePlateScript>();
        engine?.OnMove(context);

        if (!gun && !engine)
        {
            Vector2 movement = context.ReadValue<Vector2>();


            if (maincamera)
            {
                move = maincamera.GetComponent<CameraRelativeMovement>().GetCameraRelativeMovement(movement);

            }
        }
    }
    public void onShip()
    {
        if (!transform.parent)
        {
            RaycastHit hit;
            Ray r = new Ray(transform.position, -transform.up);
            Physics.Raycast(r, out hit);
            Debug.DrawRay(r.origin, r.direction,Color.yellow,1);
            if (hit.collider.name == "Floor")
            {
                transform.parent = hit.collider.transform;
            }
        }


    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (rb?.velocity.y == 0)
        {
MovementActive = true;
                   
            rb.AddForce(transform.up*jumpHeight,ForceMode.Impulse);
        }
        Debug.Log("Jump!");
        
        
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
        collider.transform.gameObject.GetComponentInChildren<GunPressurePlate>()?.AttachPlayer(gameObject);
    }

    private void DetachFromGunPressurePlate(Collider collider)
    {
        collider.transform.gameObject.GetComponentInChildren<GunPressurePlate>()?.DetachPlayer(gameObject);
    }
    private void AttachToEnginePressurePlate(Collider collider)
    {
        collider.transform.gameObject.GetComponentInChildren<EnginePressurePlateScript>()?.AttachPlayerToEnginePressurePlate(gameObject);
    }
    private void DetachFromEnginePressurePlate(Collider collider)
    {
        collider.transform.gameObject.GetComponentInChildren<EnginePressurePlateScript>()?.DetachPlayerFromEnginePressurePlate(gameObject);
        move = Vector3.zero;
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
    private GameObject maincamera;
    public Rigidbody rb;

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
