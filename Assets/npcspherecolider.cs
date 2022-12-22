using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcspherecolider : MonoBehaviour
{
    public DialogueTrigger dt;
    public PlayerController playerController;

    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        var playercontroller = other.gameObject.GetComponentInParent<PlayerController>();
        if (playercontroller)
        {
            playercontroller.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var playercontroller = other.gameObject.GetComponentInParent<PlayerController>();
        if (playercontroller)
        {
            playercontroller.gameObject.transform.parent = null;
            EndDialogue();
        }
    }
    public void TriggerDialogue()
    {
        dt.TriggerDialogue();
    }
    public void EndDialogue()
    {
        dt.EndDialogue();
    }
}
