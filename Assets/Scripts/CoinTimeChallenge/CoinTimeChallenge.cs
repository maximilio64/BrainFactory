using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinTimeChallenge : MonoBehaviour, Tick
{
    public int maxTimeLimit = 5;
    int currentTime;
    bool doingChallenge = false;
    int totalCoins;
    int currentCoins = 0;

    Coin[] coins;
    ChallengeStart challengeStart;

    DialogueBox dialogueBox;

    GameObject platform;

    bool won = false;

    PlayerController playerController;

    public void BeginChallenge()
    {
        GameObject.Find("Player/CameraRotator/Main Camera").GetComponent<PlayerTicker>().active = true;
        doingChallenge = true;
        currentTime = maxTimeLimit;
        currentCoins = 0;
        foreach (Coin c in coins)
        {
            c.gameObject.SetActive(true);
        }
        UpdateDisplay();
    }

    public void GotCoin()
    {
        currentCoins++;
        UpdateDisplay();

        if (currentCoins == totalCoins)
        {
            dialogueBox.AddDialogue("I’ve manifested a bridge to new places!", false);
            platform.SetActive(true);
            platform.AddComponent<AudioSource>().PlayOneShot(playerController.bamboo);
            challengeText.text = "";
            doingChallenge = false;
            GameObject.Find("Player/CameraRotator/Main Camera").GetComponent<PlayerTicker>().active = false;
        }
    }

    public void tick(int everyOther)
    {
        if (doingChallenge)
        {
            currentTime--;
            UpdateDisplay();
            if (currentTime == 0)
            {
                foreach (Coin c in coins)
                {
                    c.gameObject.SetActive(false);
                }
                challengeText.text = "";
                challengeStart.gameObject.SetActive(true);
                doingChallenge = false;
                GameObject.Find("Player/CameraRotator/Main Camera").GetComponent<PlayerTicker>().active = false;
            }
        }
    }

    public Text challengeText;

    public void UpdateDisplay()
    {
        challengeText.text = currentTime + "\n" + currentCoins + "/" + totalCoins;
    }

    // Start is called before the first frame update
    void Awake()
    {
        coins = GetComponentsInChildren<Coin>();
        totalCoins = coins.Length;
        foreach (Coin c in coins)
        {
            c.gameObject.SetActive(false);
        }
        challengeStart = transform.GetComponentInChildren<ChallengeStart>();
        challengeText.text = "";

        platform = transform.Find("Platform").gameObject;
        platform.SetActive(false);
    }

    private void Start()
    {
        dialogueBox = FindObjectOfType<DialogueBox>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
