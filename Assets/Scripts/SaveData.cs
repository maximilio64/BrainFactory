using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveData
{
    //powerups
    public static bool hasDoubleJumpPower = true;

    public static bool hasAttackPower = false;
    public static bool hasAttackPowerUpgrade = true;

    public static bool hasPlatformPower = true;
    public static bool hasPlatformPowerUpgrade = true;

    public static int FactoryScore()
    {
        int score = 0;
        if (hasDoubleJumpPower) score++;
        if (hasAttackPower) score++;
        if (hasAttackPowerUpgrade) score++;
        if (hasPlatformPower) score++;
        if (hasPlatformPowerUpgrade) score++;
        return score;
    }

    //collectables
    public static int lives = 3;
    public static int orbs = 0;
}
