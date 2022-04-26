using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Progress {
    attack, platform, points
}

public class Change : MonoBehaviour
{
    public int pointsNeeded = 5;
    public Progress progressNeeded;

    public bool shouldBeActive()
    {
        if (progressNeeded == Progress.attack && SaveData.hasAttackPower)
            return true;
        if (progressNeeded == Progress.platform && SaveData.hasPlatformPower)
            return true;
        if (progressNeeded == Progress.points && SaveData.FactoryScore() >= pointsNeeded)
            return true;
        return false;
    }
}
