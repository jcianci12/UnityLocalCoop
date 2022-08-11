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

    // Start is called before the first frame update
    private void Awake()
    {
        if (playerPrefab != null)
        {
            playerController = GameObject.Instantiate(playerPrefab, startPos, transform.rotation).GetComponent<PlayerController>();
            transform.parent = playerController.transform;
            //playerController.gameObject.tag = "PlayerPrefab";
            //var go = GameObject.FindGameObjectWithTag("GameManager");
            //var gm = go.GetComponent<GameManagerScript>();
            //gm.PlayerJoined(playerController.gameObject);
        }
    }

    public void AttachPlayerToPressurePlate(PressurePlate pressurePlate,GameObject go)
    {
        pressurePlate.AttachPlayer(go);
        pp = pressurePlate;
    }
    public void DetachPlayerToPressurePlate(PressurePlate pressurePlate,GameObject go)
    {
        pressurePlate.DetachPlayer(go);
        pp = null;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (pp != null)
        {
            pp.OnMove(context);
        }
        else
        {
            playerController.OnMove(context);

        }
        //if we are attached to a gun

    }
    

}
