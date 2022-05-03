using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    private Queue<string> dialogueStrings = new Queue<string>();

    public void AddDialogue(string d, bool neverRepeat)
    {
        if (neverRepeat && SaveData.pastDialogue.Contains(d))
            return;
        if (dialogueStrings.Count == 0 || d != dialogueStrings.Peek()) //dont add duplicate dialogue
        {
            dialogueStrings.Enqueue(d);
            SaveData.pastDialogue.Add(d);
        }
    }

    Text displayText;
    Image displayBox;

    float textProgress = 0;

    private void Start()
    {
        displayText = GetComponentInChildren<Text>();
        displayBox = GetComponent<Image>();
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
