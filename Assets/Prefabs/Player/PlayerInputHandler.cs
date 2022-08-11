using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public GameObject playerPrefab;


    public PlayerController playerController;
    Vector3 startPos = new Vector3(0, 1, 0);
    public PressurePlate pp = null;
    public EnginePressurePlateScript epp = null;

    // Start is called before the first frame update
    private void Awake()
    {
        if (playerPrefab != null)
        {
            playerController = GameObject.Instantiate(playerPrefab, startPos, transform.rotation).GetComponent<PlayerController>();
            transform.parent = playerController.transform;            
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (pp != null)
        {
            pp.OnMove(context);
        }
        if (epp != null)
        {
            epp.OnMove(context);
        }
        else
        {
            playerController.OnMove(context);
        }
        
        //if we are attached to a gun
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        playerController.Jump(context);

    }



}
