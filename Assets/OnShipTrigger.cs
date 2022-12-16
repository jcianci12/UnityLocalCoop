using ProjectDawn.SplitScreen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnShipTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator CameraAnimator;
    public GameObject ship;
    public SplitScreenEffect sse;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<PlayerController>())
        {

            CameraAnimator.SetBool("ZoomedIn", false);
            other.gameObject.GetComponentInParent<PlayerController>().transform.SetParent(ship.transform,true);

        }
    } 
    private void OnTriggerExit(Collider other)
    {
           if (other.gameObject.GetComponentInParent<PlayerController>())
        {
            CameraAnimator.SetBool("ZoomedIn", true);
            other.gameObject.GetComponentInParent<PlayerController>().transform.SetParent(null, true);


        }
    }
}
