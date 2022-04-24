using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    public Text livesText;

    void SetLivesText()
    {
        livesText.text = "Lives: " + SaveData.lives;
    }
    public void ChangeLives(int amount)
    {
        SaveData.lives += amount;
        SetLivesText();
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
