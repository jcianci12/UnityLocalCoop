using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundedTrigger : MonoBehaviour
{
    public PlayerController pc;
    private int groundedobjectcount = 0;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        groundedobjectcount++;
        if (groundedobjectcount > 0)
        {
            pc.isGrounded = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        groundedobjectcount--;
        if (groundedobjectcount == 0)
        {
            pc.isGrounded = false;

        }


    }
}
