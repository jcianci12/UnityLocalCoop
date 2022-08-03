using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public GameObject playerPrefab;

    PlayerController playerController;

    // Start is called before the first frame update
    private void Awake()
    {
        if (playerPrefab != null)
        {
            playerController = playerPrefab.GetComponent<PlayerController>();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        playerController.OnMove (context);
    }
}
