using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public PlayerControls PlayerControls;
    private PlayerInputHandler playerInputHandler;

    private Rigidbody rb;

    private Vector3 playerVelocity;

    private bool groundedPlayer;

    [SerializeField]
    public GameObject controlledObject;
    [SerializeField]
    public bool MovementActive = true;

    [Header("Player")]
    [SerializeField]
    private float playerSpeed = 20f;

    public float jumpHeight = 1f;

    private Vector3 move;
    public float torque = 10f;

    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        cam = Camera.main;
        camPivot = Camera.main;
        //rb = controller.GetComponentInChildren<Rigidbody>();
        PlayerControls = new PlayerControls();
        //transform.parent = GameObject.FindGameObjectWithTag("Ship").transform;

    }

    void FixedUpdate()
    {


    }
    public void Update()
    {
        if (heldObj != null)
        {
            //move object
            MoveObject(heldObj, holdArea, pickupForce);

        }
        if (MovementActive)
        {
            //controller.enabled = true;

            ////check if player is grounded
            ////if the player is grounded and and velocity is more than 0
            //if (groundedPlayer && playerVelocity.y < 0)
            //{
            //    //set the player velocity to zero
            //    playerVelocity.y = 0f;
            //}
            ////apply gravity
            //playerVelocity.y += gravityValue * Time.deltaTime;

            ////inherit the movement from the parent
            ////move = gameObject.transform.parent.position + move;
            //controller.Move((move) * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero)
            {
                //var direction = gameObject.transform.position - move;
                Debug.DrawLine(gameObject.transform.forward, move);
                Vector3 movedir = transform.TransformDirection(move * playerSpeed);
                //gameObject.GetComponent<Rigidbody>().velocity = new Vector3(move.x, 0, move.z);

                transform.forward = move;

                rb.AddForce(new Vector3(move.x, 0, move.z).normalized * playerSpeed);
                //var rotation = Quaternion.FromToRotation(gameObject.transform.forward, move).eulerAngles * torque;
                //gameObject.GetComponent<Rigidbody>().AddTorque(rb.velocity);



                //rb.transform.rotation = Quaternion.LookRotation(rb.velocity, transform.up);
                //gameObject.transform.forward = move;
                //gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(0,move.x,0)*torque);
                //shipRigidBody.gameObject.GetComponent<Rigidbody>().AddTorque(transform.up * move.x * torque);
                //controller.GetComponent<Rigidbody>().AddForce(move);


            }


            //controller.Move(playerVelocity * Time.deltaTime);
            //controller.enabled = false;


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
                move = maincamera.GetComponent<CameraRelativeMovement>().GetCameraRelativeMovement(movement).normalized;

            }
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump!");
        if (rb)
        {
            if (epp)
            {
                rb.isKinematic = false;
                rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            }
        }



        //if (groundedPlayer)
        //{
        //    //playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //    //rigidbody.AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
        //}
    }


    public void OnTriggerEnter(Collider other)
    {
        switch (other.name)
        {
            case "GunPressurePlate":
                AttachToGunPressurePlate(other);
                break;

            case "EnginePressurePlate":
                if (!epp)
                {
                    AttachToEnginePressurePlate(other);

                }
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
                if (epp)
                {
                    DetachFromEnginePressurePlate(other);

                }
                break;

        }
    }

    private void AttachToGunPressurePlate(Collider collider) //the pressure plate
    {
        gpp = collider.GetComponent<GunPressurePlate>();
        if (gpp.PlayerRigidBody == null)
        {
            gpp.AttachPlayer(gameObject);
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
        }
    }
    private void AttachToEnginePressurePlate(Collider collider)
    {
        epp = collider.GetComponent<EnginePressurePlateScript>();
        if (epp.playerRigidBody == null)
        {
            epp.AttachPlayer(gameObject);
            //check there is no one on the pressure plate
            this.MovementActive = false;
            //controller.transform.parent = collider.transform;
            move = Vector3.zero;
            rb.isKinematic = true;

        }

        //playerInputHandler.AttachPlayerToPressurePlate(collider.GetComponent<PressurePlate>(), gameObject);
    }
    private void DetachFromEnginePressurePlate(Collider collider)
    {

        var epp = collider.GetComponent<EnginePressurePlateScript>();
        var playeronpressureplateinstanceid = epp.playerRigidBody.gameObject.GetInstanceID();
        var colliderplayerinstanceid = gameObject.GetInstanceID();
        if (playeronpressureplateinstanceid == colliderplayerinstanceid)
        {

            epp.DetachPlayer(gameObject);
            playerInputHandler = null;
            rb.isKinematic = false;

        }
    }

    [Header("PIckup Settings")]
    [SerializeField] Transform holdArea;
    private GameObject heldObj;
    private Rigidbody heldObjRB;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 1.0f;
    [SerializeField] private float pickupForce = 150f;
    private EnginePressurePlateScript epp;
    private GunPressurePlate gpp;

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
    void MoveObject(GameObject child, Transform holdarea, float pickupForce)
    {
        if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }




}
