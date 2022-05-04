using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveData
{
    //powerups
    //public static bool hasDoubleJumpPower = true;

    //public static bool hasAttackPower = true;
    //public static bool hasAttackPowerUpgrade = true;

    //public static bool hasPlatformPower = true;
    //public static bool hasPlatformPowerUpgrade = true;

    public static bool hasDoubleJumpPower = false;

    public static bool hasAttackPower = true;
    public static bool hasAttackPowerUpgrade = true;

    public static bool hasPlatformPower = true;
    public static bool hasPlatformPowerUpgrade = true;

    public static bool completedCredits = true;

    public static List<string> pastDialogue = new List<string>();

    public static List<int> deleteObjectsWithTheseIDs = new List<int>();
    public static List<int> deletePowerupsWithTheseIDs = new List<int>();

    public static Vector3 playerBrainStartLoc = new Vector3(-176.550003f, 14.2799997f, -108.690002f);

    public static int FactoryScore()
    {
        int score = 0;
        if (hasDoubleJumpPower) score++;
        if (hasAttackPower) score++;
        if (hasAttackPowerUpgrade) score++;
        if (hasPlatformPower) score++;
        if (hasPlatformPowerUpgrade) score++;
        if (completedCredits) score++;
        return score;
    }

    public static int TotalPossibleScore() {
        return 5;
    }

    public static float Ratio()
    {
        return (float)TotalPossibleScore() / (float)FactoryScore();
    }

    public static float Ratio2()
    {
        return ((float)FactoryScore()) / ((float)TotalPossibleScore());
    }

    public static bool canSkipTitle = false;

    //collectables
    public static int lives = 3;
    public static int orbs = 0;

    public static int usedOrbs = 0;
}
