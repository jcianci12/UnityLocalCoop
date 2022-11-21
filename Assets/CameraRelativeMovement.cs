using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRelativeMovement : MonoBehaviour
{
    
    public Vector3 GetCameraRelativeMovement(Vector2 movement)
    {
        float vertical = movement.y;
        float horizontal = movement.x;

        Vector3 forward = gameObject.transform.forward;
        Vector3 right = gameObject.transform.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardRelativeVerticalInput = vertical * forward;
        Vector3 rightRelativeHorizontalInput = horizontal * right;

        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeHorizontalInput;
        //Debug.Log(cameraRelativeMovement.x + " " + cameraRelativeMovement.y);
        Debug.DrawLine(cameraRelativeMovement, Vector3.zero);
        //move = new Vector3(movement.x * right.x, 0, movement.y * forward.y);
        return cameraRelativeMovement;
    }
}
