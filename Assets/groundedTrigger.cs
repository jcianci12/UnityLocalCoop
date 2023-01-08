using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundedTrigger : MonoBehaviour
{
    public PlayerController pc;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        pc.isGrounded = true;
    }
    private void OnTriggerExit(Collider other)
    {
        pc.isGrounded =false;
    }
}
