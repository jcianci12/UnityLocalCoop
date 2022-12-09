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

    public float jumpHeight = 500f;
    private float gravityValue = -99.81f;

    private Vector3 move;
    public float m_Thrust = 20f;
    //test
    public bool CargoInRange;
    public float CargoPickupRange = 1f;
    public LayerMask cargoLayer;
    private float distToGround;


    private void Awake()
    {
        cam = Camera.main;
        camPivot = Camera.main;


    }
    private void Start()
    {
        distToGround = gameObject.GetComponentInChildren<Collider>().bounds.extents.y;
    }

    void FixedUpdate()
    {
        

        onShip();
        
        gameObject.transform.forward = move;

        if (move != Vector3.zero && MovementActive)
        {
            rb.AddForce(move * playerSpeed);
            //get the velocity of the ship
            var shipvel = transform.parent ?. GetComponentInParent<Rigidbody>().velocity;
            rb.AddForce(shipvel??Vector3.zero);

        }


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

        //if (!gun && !engine)
        //{
            Vector2 movement = context.ReadValue<Vector2>();


            if (maincamera)
            {
                move = maincamera.GetComponent<CameraRelativeMovement>().GetCameraRelativeMovement(movement);

            }
        //}
    }
    public void onShip()
    {
        if (!transform.parent)
        {
            RaycastHit hit;
            Ray r = new Ray(transform.position, -transform.up);
            Physics.Raycast(r, out hit);
            Debug.DrawRay(r.origin, r.direction, Color.yellow, 1);
            if (hit.collider.name == "Floor")
            {
                transform.parent = hit.collider.transform;
                //rb.velocity = transform.parent.GetComponentInParent<Rigidbody>().velocity;
                
            }
        }


    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (true)
        {
            Debug.Log("is grounded");
            MovementActive = true;
            rb.AddForce(0,jumpHeight,0);
        }
        Debug.Log("Jump!");
    }
    public void Fire(InputAction.CallbackContext context)
    {
        transform.parent?.GetComponent<GunPressurePlate>()?.Fire();
        PickupCargo();

        Debug.Log("Fire!");
    }
    bool isGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + new Vector3(0, -1, 0), Color.yellow);
        return Physics.Raycast(transform.position, transform.position + new Vector3(0, -1, 0), distToGround + 0.1f);
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
    public GameObject maincamera;
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
