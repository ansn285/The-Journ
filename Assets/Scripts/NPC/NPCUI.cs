using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCUI : MonoBehaviour
{
    // Global Rule: At the end of tevery sentence if there is ~ then that means this is a dialogue
    // If there is ` at the end of a sentence, that means its a choice.
    // If there is . at the end, then that's the last sentence

    [Header("Canvas section")]
    public Canvas interactionCanvas;
    public Canvas dialogueCanvas;

    [Header("Dialogue section")]
    public string[] sentences;
    private Dialogue dialogue;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogue = new Dialogue(sentences);
            interactionCanvas.gameObject.SetActive(true);
            interactionCanvas.GetComponent<Animator>().SetTrigger("Collided");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !dialogueCanvas.enabled)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueCanvas.GetComponent<DialogueManager>().StartDialogue(dialogue);

                collision.GetComponent<CharacterMovement>().enabled = false;
                dialogueCanvas.enabled = true;
                dialogueCanvas.GetComponent<Animator>().SetTrigger("Enter");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionCanvas.GetComponent<Animator>().SetTrigger("Exit");
            Invoke("DisableInteractionCanvas", 0.2f);
        }
    }

    private void DisableInteractionCanvas()
    {
        interactionCanvas.gameObject.SetActive(false);
    }

}
