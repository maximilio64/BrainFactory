using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    public Text livesText;

    void SetLivesText()
    {
        livesText.text = "Lives: " + SaveData.lives;
    }
    public void ChangeLives(int amount)
    {
        GetComponent<PlayerController>().HurtSound();
        SaveData.lives += amount;
        SetLivesText();
        if (SaveData.lives <= 0)
        {
            GetComponent<PlayerController>().DeathSound();
            SaveData.orbs += SaveData.usedOrbs;
            SaveData.usedOrbs = 0;
            SaveData.lives = 3;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Start()
    {
        SetLivesText();
        SetOrbsText();
    }

    public Text orbsText;

    void SetOrbsText()
    {
        orbsText.text = "Orbs: " + SaveData.orbs;
    }
    public void ChangeOrbs(int amount)
    {
        SaveData.orbs += amount;
        SetOrbsText();
    }
}
