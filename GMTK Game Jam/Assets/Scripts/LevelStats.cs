using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelStats
{
    private static int nextLevel = 1;

    public static int NextLevel
    {
        get { return nextLevel; }

        set { nextLevel = value; }
    }

    public static void incrementLevel()
    {
        nextLevel++;
    }
}
