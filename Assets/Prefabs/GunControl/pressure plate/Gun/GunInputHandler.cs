using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunInputHandler : MonoBehaviour
{
    public GameObject gunPrefab;
    //how does this get connected?
    public CharacterController gunController;
    public PressurePlate pp;



    public void PlayerConnected(PlayerController  pp,GameObject player)
    {
        gunController = player.GetComponent<CharacterController>();
        //pp = new PressurePlate();
        //pp = go.GetComponent<PressurePlate>();
    } 
    public void OnMove(InputAction.CallbackContext context)
    {
        //gunController.OnMove(context);
    }
}
