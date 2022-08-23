using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public GameObject playerPrefab;


    public PlayerController playerController;
    Vector3 startPos;
    public PressurePlate gunpressureplate = null;
    public EnginePressurePlateScript enginepressureplate = null;
    private Camera maincam;

    // Start is called before the first frame update
    private void Start()
    {
    }

    private void Awake()
    {
        if (playerPrefab != null)
        {
            var ship = GameObject.FindGameObjectWithTag("Ship");
            startPos = new Vector3(ship.transform.position.x, 1, ship.transform.position.y);

            playerController = GameObject.Instantiate(playerPrefab, startPos, transform.rotation).GetComponent<PlayerController>();
            playerController.transform.parent = GameObject.FindGameObjectWithTag("Ship").transform;
            transform.parent = playerController.transform;

        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (gunpressureplate != null)
        {
            gunpressureplate.OnMove(context);
        }
        if (enginepressureplate != null)
        {
            enginepressureplate.OnMove(context);
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
    public void OnFire(InputAction.CallbackContext context)
    {
        if (enginepressureplate != null)
        {
            enginepressureplate.Fire();

        }
    }


}
