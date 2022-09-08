using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public GameObject playerPrefab;


    public PlayerController playerController;
    Vector3 startPos;
    public GunPressurePlate gunpressureplate = null;
    public EnginePressurePlateScript enginepressureplate = null;
    private Camera maincam;
    //public GameObject SpawnPoint;

    // Start is called before the first frame update
    private void Start()
    {
    }

    private void Awake()
    {
        if (playerPrefab != null)
        {
            var spawnpoint = GameObject.Find("SpawnPoint");
            startPos = spawnpoint.transform.position;

            playerController = GameObject.Instantiate(playerPrefab, startPos, transform.rotation).GetComponent<PlayerController>();
            playerController.transform.parent = spawnpoint.transform;
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
        if (gunpressureplate != null)
        {
            Debug.Log("Firing from input handler");

            gunpressureplate.Fire();

        }
    }


}
