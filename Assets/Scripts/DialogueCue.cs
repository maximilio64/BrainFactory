using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCue : MonoBehaviour
{
    [SerializeField]
    public string dialogueString;
    [SerializeField]
    public bool neverRepeat = true;

    DialogueBox dialogueBox;
    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = FindObjectOfType<DialogueBox>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialogueBox.AddDialogue(dialogueString, neverRepeat);
            Destroy(this.gameObject);
        }
    }
}
