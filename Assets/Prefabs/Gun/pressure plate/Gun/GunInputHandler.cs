using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunInputHandler : MonoBehaviour
{
    public GameObject gunPrefab;
    //how does this get connected?
    public CharacterController gunController;
    public GunPressurePlate pp;
    private Vector3 move;
    public Camera cam;



    public void PlayerConnected(CharacterController  playerPrefab,GameObject gun)
    {

        //we have the controller of the player
        gunController = playerPrefab.GetComponent<CharacterController>();


        //pp = new PressurePlate();
        //pp = go.GetComponent<PressurePlate>();
    }
    public void Update()
    {

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = cam.GetComponent<CameraRelativeMovement>().GetCameraRelativeMovement(move);
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("move!");

        //gunController.OnMove(context);
    }
}
