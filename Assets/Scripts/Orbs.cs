using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbs : MonoBehaviour
{

    public bool isPowerup = false;

    DialogueBox dialogueBox;

    Control control;

    Vector3 centralPos;

    PlayerController playerController;

    private void Start()
    {
        control = (Control)GameObject.FindObjectOfType<Control>();
        centralPos = transform.position;

        dialogueBox = FindObjectOfType<DialogueBox>();
        playerController = FindObjectOfType<PlayerController>();

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (!isPowerup)
            {
                control.ChangeOrbs(1);
                playerController.playCoinSound();
                Destroy(this.gameObject);
            } else
            {
                dialogueBox.AddDialogue("I feel stronger. Press e to release a vital aura.");
                Destroy(this.gameObject);
                playerController.hasPowerup = true;
                playerController.changeMusic();
            }

        }
    }

    private void Update()
    {
        Random.seed = GetInstanceID();
        transform.position = centralPos + new Vector3(0, Mathf.Sin((Time.realtimeSinceStartup + Random.Range(0, 2f))) / 2f + 1f, 0);
    }
}
