using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    private Queue<string> dialogueStrings = new Queue<string>();

    public void AddDialogue(string d)
    {
        dialogueStrings.Enqueue(d);
    }

    Text displayText;
    Image displayBox;

    float textProgress = 0;

    private void Start()
    {
        displayText = GetComponentInChildren<Text>();
        displayBox = GetComponent<Image>();

        dialogueStrings.Enqueue("The desert, so desolate, yet maybe my mind will find inspiration in the introspection one gains from looking into nothingness.");
    }

    private void Update()
    {
        if (dialogueStrings.Count > 0)
        {
            displayBox.enabled = true;
            displayText.enabled = true;
            displayText.text = dialogueStrings.Peek().Substring(0, Mathf.Min((int)textProgress, dialogueStrings.Peek().Length));
            if (dialogueStrings.Peek().Length + 20 > textProgress)
            {
                textProgress += Time.deltaTime * 10;
            } else
            {
                dialogueStrings.Dequeue();
                textProgress = 0;
            }
        } else
        {
            displayBox.enabled = false;
            displayText.enabled = false;
        }
    }

}
