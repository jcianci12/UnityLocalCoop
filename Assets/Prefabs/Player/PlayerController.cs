using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;
using ProjectDawn.SplitScreen;


public class PlayerController : MonoBehaviour
{

    public PlayerControls PlayerControls;
    private PlayerInputHandler playerInputHandler;
    private InputAction jump;




    [SerializeField]
    public GameObject controlledObject;
    [SerializeField]
    public bool MovementActive = true;

    [Header("Player Stats")]
    [SerializeField]
    private float playerAccelleration;
    public float speedLimit;
    public float jumpHeight = 5f;
    public float fallMultiplier = 2.0f;



    private Vector3 move;
    public float m_Thrust = 20f;
    //test
    public bool CargoInRange;
    public float CargoPickupRange = 1f;
    public LayerMask cargoLayer;
    private float distToGround;

    public Rigidbody rb;




    [Header("PIckup Settings")]
    [SerializeField] Transform holdArea;
    private GameObject heldObj;
    private Rigidbody heldObjRB;
    public Transform held;
    public GameObject objectInArea;


    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 1.0f;
    [SerializeField] private float pickupForce = 150f;

    [Header("Camera Parameters")]
    public GameObject maincamera;
    private SplitScreenEffect sse;
    public GameManagerScript gameManagerScript;
    public Camera prefabSplitScreenCam;
    ScreenData sd;
    public float shipFieldOfView;
    public float originalFieldOfView;
    private int screenIndex;
    public Animator CameraAnimator;

    public bool isGrounded;
    private void Awake()
    {

        sse = GameObject.FindObjectOfType<SplitScreenEffect>();
        Destroy(sse);
        Destroy(GameObject.FindObjectOfType<Camera>());
        maincamera = Instantiate(prefabSplitScreenCam).gameObject;
        sse = GameObject.Find("Split Screen").GetComponent<SplitScreenEffect>();
        originalFieldOfView = sse.GetComponent<Camera>().orthographicSize;


        if (sse.Screens.Any(i => i.Target.GetComponent<PlayerController>() == null))
        {
            ScreenData sd = new ScreenData { Camera = maincamera.GetComponent<Camera>(), Target = gameObject.transform };

            screenIndex = 0;
            sse.Screens[screenIndex] = sd;
        }
        else
        {
            ScreenData sd = new ScreenData { Camera = maincamera.GetComponent<Camera>(), Target = gameObject.transform };
            sse.AddScreen(sd.Camera, sd.Target);
            screenIndex = sse.Screens.Count() - 1;
        }
    }
    private void Start()
    {
        distToGround = gameObject.GetComponentInChildren<Collider>().bounds.extents.y;
    }

    void FixedUpdate()
    {

        gameObject.transform.forward = move;

        if (move != Vector3.zero && MovementActive)
        {
            //Debug.Log(rb.velocity.magnitude);
            //speed limit
            if (rb.velocity.magnitude < speedLimit && isGrounded)
            {
                rb.AddForce(move * playerAccelleration);
            }
            
            //get the velocity of the ship
            var shipvel = transform.parent?.GetComponentInParent<Rigidbody>()?.velocity;
            rb.AddForce(shipvel ?? Vector3.zero);
        }
        else
        {
            //if the player is moving but the joystick is not
            if (rb.velocity.x != 0 && rb.velocity.z !=0)
            {
                rb.velocity.Set(0,rb.velocity.y,0);
            }
        }
        //Fall speed
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
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
            if (movement != Vector2.zero)
            {
                move = maincamera.GetComponent<CameraRelativeMovement>().GetCameraRelativeMovement(movement);
            }


        }

        //}
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            Debug.Log("is grounded");
            MovementActive = true;
            rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
        }
        Debug.Log("Jump!");
    }


    public void Fire(InputAction.CallbackContext context)
    {
        transform.parent?.GetComponent<GunPressurePlate>()?.Fire();
        transform.parent?.GetComponent<npcspherecolider>()?.TriggerDialogue();
        PickupCargo();

        Debug.Log("Fire!");
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
        collider.transform.gameObject.GetComponentInChildren<GunPressurePlate>()?.AttachPlayerToGunPressurePlate(gameObject);
    }

    private void DetachFromGunPressurePlate(Collider collider)
    {
        collider.transform.gameObject.GetComponentInChildren<GunPressurePlate>()?.DetachPlayerFromGunPressurePlate(gameObject);
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




    public void PickupCargo()
    {
        if (heldObj == null)
        {
            if (objectInArea != null)
            {
                //
                PickupObject(objectInArea.transform.gameObject);
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
