using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunPressurePlate : MonoBehaviour
{
    // Start is called before the first frame update

    //[SerializeField]
    //GameObject gun;
    public static GameObject playerobject;
    public GunInputHandler gunInputHandler;
    public GameObject ejectionPoint;
    private Vector3 move;
    public GameObject gun;
    GameObject player;
    public GameObject ship;


    private void Start()
    {

    }
    public void FixedUpdate()
    {
        if (move != Vector3.zero)
        {
            gun.transform.forward = move;
            gun.transform.Rotate(Vector3.up, -90);
        }


    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();

        move = player.GetComponentInChildren<PlayerController>().maincamera.GetComponent<CameraRelativeMovement>().GetCameraRelativeMovement(
        new Vector3(movement.y, -movement.x, 0)
        );
        Debug.Log(move);

    }
    //public void AttachPlayer(GameObject player)
    //{
    //    var p = player.GetComponentInChildren<PlayerController>();
    //    if (p)
    //    {
    //        player.transform.parent = transform;
    //        p.MovementActive = false;
    //    }
    //}
    //public void DetachPlayer(GameObject player)
    //{
    //    var p = player.GetComponentInChildren<PlayerController>();
    //    if (player.transform.IsChildOf(transform))
    //    {
    //        player.transform.parent = null;
    //        p.MovementActive = true;
    //    }
    //}


    public void AttachPlayerToGunPressurePlate(GameObject go)
    {

        if (transform.GetComponentsInChildren<PlayerController>().Length == 0)
        {
            //make the player a child
            player = go;
            player.transform.parent = transform;
            player.GetComponent<PlayerController>().MovementActive = false;

        }
    }
    public void DetachPlayerFromGunPressurePlate(GameObject go)
    {
        //check if the pressure plate has this player attached
        if (go.transform.IsChildOf(transform))
        {
            player.transform.parent = null;
            player.GetComponent<PlayerController>().MovementActive = true;
            move = Vector3.zero;

            player.transform.parent = ship.transform;
            player = null;
        };

    }

    public GameObject projectile;
    public void Fire()
    {
        Debug.Log("fire!");
        //rotate the projectile 90 degrees on the x
        Quaternion rotation = gun.transform.rotation;

        GameObject ball = Instantiate(projectile, ejectionPoint.transform.position,
                                                     rotation);
        ball.transform.Rotate(90, 0, 0);
        ball.GetComponent<Rigidbody>().velocity = (ejectionPoint.transform.forward * ball.GetComponent<projectile>().velocity);


    }

}
