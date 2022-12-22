using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator dialogueAnimator;
    public Animator interactAnimator;

    Queue<string> sentences;

    // Start is called before the first frame update
   
    void Start()
    {
        interactAnimator.SetBool("IsOpen", true);
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue) {
        interactAnimator.SetBool("IsOpen", false);
        dialogueAnimator.SetBool("IsOpen",true);
        Debug.Log("Starting");
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
    StartCoroutine(TypeSentence(sentence));    
    }
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            yield return null;
        }
    }
    public void EndDialogue() {
        dialogueAnimator.SetBool("IsOpen", false);
        interactAnimator.SetBool("IsOpen", true);

        Debug.Log("End"); }
    
}
