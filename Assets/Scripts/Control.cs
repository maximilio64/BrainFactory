using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    public Text livesText;
    public int lives = 3;

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives;
    }
    public void ChangeLives(int amount)
    {
        lives += amount;
        SetLivesText();
    }

    private void Start()
    {
        SetLivesText();
        SetOrbsText();
    }

    public Text orbsText;
    public int orbs = 0;

    void SetOrbsText()
    {
        orbsText.text = "Orbs: " + orbs;
    }
    public void ChangeOrbs(int amount)
    {
        orbs += amount;
        SetOrbsText();
    }
}
