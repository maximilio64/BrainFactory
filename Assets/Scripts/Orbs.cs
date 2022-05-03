using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbs : MonoBehaviour
{

    DialogueBox dialogueBox;

    Control control;

    Vector3 centralPos;

    PlayerController playerController;

    public bool noID = false; //used only for used orbs currently

    public int coinID;
    public void Reset()
    {
        coinID = UnityEngine.Random.Range(1, 10000) * GetInstanceID();
    }

    private void Start()
    {
        if (!noID && SaveData.deleteObjectsWithTheseIDs.Contains(coinID))
            Destroy(this.gameObject);
        control = (Control)GameObject.FindObjectOfType<Control>();
        centralPos = transform.position;

        dialogueBox = FindObjectOfType<DialogueBox>();
        playerController = FindObjectOfType<PlayerController>();

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            control.ChangeOrbs(1);
            if (noID)
                SaveData.usedOrbs--;
            playerController.playCoinSound();
            SaveData.deleteObjectsWithTheseIDs.Add(coinID);
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        Random.seed = GetInstanceID();
        transform.position = centralPos + new Vector3(0, Mathf.Sin((Time.realtimeSinceStartup + Random.Range(0, 2f))) / 2f + 1f, 0);
    }
}
