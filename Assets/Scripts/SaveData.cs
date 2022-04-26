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

    public static bool hasAttackPower = false;
    public static bool hasAttackPowerUpgrade = false;

    public static bool hasPlatformPower = false;
    public static bool hasPlatformPowerUpgrade = false;

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

    public static int TotalPossibleScore() {
        return 5;
    }

    //collectables
    public static int lives = 3;
    public static int orbs = 0;
}
