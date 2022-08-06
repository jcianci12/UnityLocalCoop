using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    // Start is called before the first frame update
   
    //[SerializeField]
    //GameObject gun;
    private static CharacterController attachedPlayer;

    public static void AttachPlayer(CharacterController playerController)
    {
        attachedPlayer = playerController;
    }
    public static void DetachPlayer(CharacterController characterController)
    {
        attachedPlayer = null;
    }

}
