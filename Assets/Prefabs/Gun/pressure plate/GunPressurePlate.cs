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
    public GunInputHandler gunInputHandler ;
    private Vector3 move;
    public GameObject gun;
    GameObject cube;

    private void Start()
    {
    }
    public void Update()
    {
        if (transform.childCount > 1)
        {
foreach(var o in gameObject.GetComponentsInChildren<PlayerController>())
        {
            //Debug.Log(o.name);
                gun.transform.forward = move;
                gun.transform.Rotate(Vector3.up,-90);  
            }
        }
        

        //if (gameObject.GetComponentInChildren<PlayerController>()!=null &&  move != Vector3.zero)
        //{
        //    gun.transform.forward = move;
        //    gun.transform.Rotate(Vector3.up,-90);            
        //}
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();       
       

        move = Camera.main.GetComponent<CameraRelativeMovement>().GetCameraRelativeMovement(
            new Vector3(movement.y, -movement.x, 0)
            );
        Debug.Log(move);
    }
    public void AttachPlayer(GameObject player)
    {
        var p = player.GetComponentInChildren<PlayerController>();
        if (p)
        {
            player.transform.parent = transform;
            p.MovementActive = false;            
        }        
    }
    public void DetachPlayer(GameObject player)
    {
        var p = player.GetComponentInChildren<PlayerController>();
        if (player.transform.IsChildOf(transform))
        {
            player.transform.parent = null;
            p.MovementActive = true;
        }
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
