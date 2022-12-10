using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcspherecolider : MonoBehaviour
{
    public DialogueTrigger dt;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        dt.TriggerDialogue();
    }
    private void OnTriggerExit(Collider other)
    {
        dt.EndDialogue();
    }

}
