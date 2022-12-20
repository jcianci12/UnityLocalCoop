using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipMap : MonoBehaviour
{
    
    public Animator animator;

    private void Start()
    {
        //animator.SetBool("LargeMapShowing", false);

    }
    private void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        //attach the player
        //heldObj = other.gameObject;
        if (other.gameObject.GetComponentInParent<PlayerController>())
        {
            animator.SetBool("IsOpen", true);
            
        }
       

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponentInParent<PlayerController>())
        {
            animator.SetBool("IsOpen", false);

        }


    }


}
    


