using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public GameObject playerPrefab;

    PlayerController playerController;
    Vector3 startPos = new Vector3(0,1,0);

    // Start is called before the first frame update
    private void Awake()
    {
        if (playerPrefab != null)
        {
            playerController = GameObject.Instantiate(playerPrefab,startPos,transform.rotation).GetComponent<PlayerController>();
            transform.parent = playerController.transform;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        playerController.OnMove (context);
    }
}
