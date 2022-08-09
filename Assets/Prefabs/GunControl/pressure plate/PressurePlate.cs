using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PressurePlate : MonoBehaviour
{
    // Start is called before the first frame update
   
    //[SerializeField]
    //GameObject gun;
    //private static CharacterController attachedPlayer;
    public GunInputHandler gunInputHandler ;
    private Vector3 move;


    public void AttachPlayer(CharacterController playerController,GameObject go,GunInputHandler gih)
    {
        //attachedPlayer = playerController;
        //var go = gameObject;
        //gunInputHandler = new GunInputHandler();
        gunInputHandler = gih;
        //gunInputHandler.PlayerConnected(playerController,go);//we should have the gun object

    }

    public void Update()
    {

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
       
            move = new Vector3(movement.x, 0, movement.y);
        
    }
    public  void DetachPlayer(CharacterController characterController)
    {
        //attachedPlayer = null;
    }

}
