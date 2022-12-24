using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public void TriggerDialogue()
    {
        var dm =  FindObjectOfType<DialogueManager>();
        if (!dm.sentencesactive)
        {
            dm.StartDialogue(dialogue);
        }
        else
        {
            dm.DisplayNextSentence();
        }
    }
    public void EndDialogue()
    {
        FindObjectOfType<DialogueManager>().EndDialogue();
    }
}
