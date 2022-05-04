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

    public int powerID;
    public void Reset()
    {
        powerID = UnityEngine.Random.Range(1, 10000) * GetInstanceID();
    }

    private void Start()
    {
        if (SaveData.deletePowerupsWithTheseIDs.Contains(powerID))
            Destroy(this.gameObject);
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
                    if (SaveData.hasAttackPower)
                    {
                        SaveData.hasAttackPowerUpgrade = true;
                        dialogueBox.AddDialogue("My vital aura has become even stronger!", true);
                    }
                    else
                    {
                        SaveData.hasAttackPower = true;
                        dialogueBox.AddDialogue("I feel stronger now, that I can overcome this place. Press e to release a vital aura.", true);
                        playerController.GetComponent<AudioSource>().clip = playerController.after;
                        playerController.GetComponent<AudioSource>().Play();
                    }
                    break;
                case PowerupType.platform:
                    if (SaveData.hasPlatformPower)
                    {
                        SaveData.hasPlatformPowerUpgrade = true;
                        dialogueBox.AddDialogue("My creative powers are expanding!", true);
                    } else
                    {
                        SaveData.hasPlatformPower = true;
                        dialogueBox.AddDialogue("This place is no longer an empty void, but a blank canvas. I can manifest anything I want out of life. Press q to manifest.", true);
                        playerController.GetComponent<AudioSource>().clip = playerController.after;
                        playerController.GetComponent<AudioSource>().Play();
                    }
                    break;
            }
            playerController.transform.Find("WalkModel").GetComponent<Animator>().SetTrigger("sew");
            playerController.soundEffectSource.PlayOneShot(playerController.power);
            SaveData.deletePowerupsWithTheseIDs.Add(powerID);
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
