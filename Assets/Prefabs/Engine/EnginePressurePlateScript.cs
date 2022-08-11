using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class EnginePressurePlateScript : MonoBehaviour
{

    private static GameObject attachedPlayer;
    public GunInputHandler gunInputHandler;
    private Vector3 move;

    public void Update()
    {

        if (move != Vector3.zero)
        {
            var parent = GameObject.FindGameObjectWithTag("Ship");
            //gameObject.transform.parent = null;

            parent.transform.forward = move;
            //gameObject.transform.SetParent(parent.transform);
            // gun.transform.forward = move;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
         Debug.Log("engine move!");
        Vector2 movement = context.ReadValue<Vector2>();

        move = new Vector3(movement.x, 0, movement.y);

    }
    public void AttachPlayer(GameObject player)
    {
        var pih = player.GetComponentInChildren<PlayerInputHandler>();
        pih.epp = gameObject.GetComponent<EnginePressurePlateScript>();
        attachedPlayer = player;
    }
    public void DetachPlayer(GameObject player)
    {
        var pih = player.GetComponentInChildren<PlayerInputHandler>();
        pih.epp = null;
        attachedPlayer = null;
    }
}
