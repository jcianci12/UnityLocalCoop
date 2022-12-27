using ProjectDawn.SplitScreen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public GameObject playerPrefab;


    public PlayerController playerController;
    Vector3 startPos;
    //public GunPressurePlate gunpressureplate = null;
    public SplitScreenEffect sse;
    public GameObject camPrefab;

    // Start is called before the first frame update
    private void Start()
    {


    }

    private void Awake()
    {
        //sse = FindObjectOfType<SplitScreenEffect>();
        if (sse != null)
        {
            Debug.Log(sse.name);

        }
        if (playerPrefab != null)
        {
            var spawnpoint = GameObject.Find("SpawnPoint");
            startPos = spawnpoint.transform.position;

            playerController = GameObject.Instantiate(playerPrefab, startPos, transform.rotation).GetComponent<PlayerController>();


            //var cam = GameObject.Instantiate(camPrefab);
            //sse.GetComponent<SplitScreenEffect>().Clear();
            //sse.GetComponent<SplitScreenEffect>().AddScreen(cam.GetComponent<Camera>(), playerController.transform);
            //playerController.maincamera = sse.Screens[0].Camera.gameObject;
            //cam.AddComponent<CameraRelativeMovement>();
            //playerController.transform.parent = spawnpoint.transform;
            //transform.parent = playerController.transform;

        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (playerController == null) return;
        playerController.OnMove(context);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed == false) return; // adding this line removes the call when the key is pressed. This fixes the problem.        {

        playerController.Jump(context);
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (playerController == null) return;

        if (context.performed == false) return; // adding this line removes the call when the key is pressed. This fixes the problem.        {
        playerController.Fire(context);


    }
}


