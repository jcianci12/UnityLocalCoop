using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PressurePlate : MonoBehaviour
{
    // Start is called before the first frame update

    //[SerializeField]
    //GameObject gun;
    private static GameObject attachedPlayer;
    public GunInputHandler gunInputHandler ;
    private Vector3 move;
    private GameObject parent;

    private void Start()
    {
        parent = gameObject.transform.parent.gameObject;
    }
    public void Update()
    {

        if (move != Vector3.zero)
        {

            parent.transform.forward = move;
           // gun.transform.forward = move;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
       // Debug.Log("move!");
        Vector2 movement = context.ReadValue<Vector2>();       
        move = new Vector3(movement.x, 0, movement.y);        
    }
    public void AttachPlayer(GameObject player)
    {
        //get the playerinput class
        var pih = player.GetComponentInChildren<PlayerInputHandler>();
        pih.pp = gameObject.GetComponent<PressurePlate>();
        attachedPlayer = player;
        gameObject.transform.parent = null;


    }
    public void DetachPlayer(GameObject player)
    {
        var pih = player.GetComponentInChildren<PlayerInputHandler>();
        pih.pp = null;
        attachedPlayer = null;
        gameObject.transform.SetParent(parent.transform);

    }

}
