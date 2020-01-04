using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] private Image dialogueWindow;
    [SerializeField] private Canvas dialogueText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueWindow.enabled = true;
            dialogueText.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueWindow.enabled = false;
            dialogueText.enabled = false;
        }
    }

}
