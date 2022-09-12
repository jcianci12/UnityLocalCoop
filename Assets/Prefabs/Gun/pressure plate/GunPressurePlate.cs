using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunPressurePlate : MonoBehaviour
{
    // Start is called before the first frame update

    //[SerializeField]
    //GameObject gun;
    private static GameObject attachedPlayer;
    public GunInputHandler gunInputHandler ;
    private Vector3 move;
    public GameObject parent;
    public GameObject gun;
    public Rigidbody PlayerRigidBody;
    GameObject cube;

    private void Start()
    {

        //cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube.transform.parent = gameObject.transform;
        //cube.transform.position = gameObject.transform.position;
        
    }
    public void Update()
    {

        if (PlayerRigidBody!=null &&  move != Vector3.zero)
        {

            gun.transform.forward = move;
            gun.transform.Rotate(Vector3.up,-90);
            //cube.transform.position = move;


            // gun.transform.forward = move;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
       // Debug.Log("move!");
        Vector2 movement = context.ReadValue<Vector2>();       
        //move = new Vector3(0 , movement.y, movement.x);
        //move = Camera.main.GetComponent<CameraRelativeMovement>().GetCameraRelativeMovement(move);


        move = Camera.main.GetComponent<CameraRelativeMovement>().GetCameraRelativeMovement(
            new Vector3(movement.y, -movement.x, 0)
            );
        Debug.Log(move);
    }
    public void AttachPlayer(GameObject player)
    {
        PlayerRigidBody = player.GetComponent<Rigidbody>();
        //get the playerinput class
        var pih = player.GetComponentInChildren<PlayerInputHandler>();
        pih.gunpressureplate = gameObject.GetComponent<GunPressurePlate>();
        attachedPlayer = player;
        //gameObject.transform.parent = null;       

    }
    public void DetachPlayer(GameObject player)
    {
        PlayerRigidBody = null;
        var pih = player.GetComponentInChildren<PlayerInputHandler>();
        pih.gunpressureplate = null;
        attachedPlayer = null;
        //gameObject.transform.SetParent(parent.transform);
        
    }
    public GameObject projectile;
    public float launchVelocity;
    public void Fire()
    {
        Debug.Log("fire!");
        //rotate the projectile 90 degrees on the x
        Quaternion rotation = gun.transform.rotation;
        
        GameObject ball = Instantiate(projectile, gun.transform.position + gun.transform.forward *2f,
                                                     gun.transform.rotation);
        ball.transform.Rotate(90, 0, 0);
        ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3
                                             (0, 0, launchVelocity));
        

    }

}
