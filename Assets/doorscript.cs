using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorscript : MonoBehaviour
{
    public GameObject door;
    private doorcontrol doorcontrol;


    // Start is called before the first frame update
    void Start()
    {
        //get the door parent object
       doorcontrol = door.GetComponent<doorcontrol>();
    }

    

    public void OnTriggerEnter(Collider other)
    {
        //door.transform
        doorcontrol.OpenDoor();
    }
    public void OnTriggerExit(Collider other)
    {
        doorcontrol.CloseDoor();
       
    }
}
