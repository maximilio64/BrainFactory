using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType
{
    jump, attack, platform
}

public class Powerup : MonoBehaviour
{

    public PowerupType powerupType;

    public GameObject mesh;

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

        GameObject myMesh = Instantiate(mesh);
        myMesh.transform.SetParent(this.transform);
        myMesh.transform.position = new Vector3(0, 0, 0);
        myMesh.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            switch (powerupType)
            {
                case PowerupType.attack:
                    SaveData.hasAttackPower = true; break;
            }
            playerController.playCoinSound();
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        Random.seed = GetInstanceID();
        transform.position = centralPos + new Vector3(0, Mathf.Sin((Time.realtimeSinceStartup + Random.Range(0, 2f))) / 2f + 1f, 0);
        transform.Rotate(Vector3.up * Time.deltaTime * 100, Space.World);
    }
}
