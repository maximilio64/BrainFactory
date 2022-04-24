using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeStart : MonoBehaviour
{
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
            if (transform.parent.GetComponent<CoinTimeChallenge>().challengeText.text != "")
            {
                dialogueBox.AddDialogue("I should complete the other time challenge I started first.");
            }
            else
            {
                transform.parent.GetComponent<CoinTimeChallenge>().BeginChallenge();
                this.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        Random.seed = GetInstanceID();
        transform.position = centralPos + new Vector3(0, Mathf.Sin((Time.realtimeSinceStartup + Random.Range(0, 2f))) / 2f + 1f, 0);
    }
}