using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LeaderboardEntry
{
    public int yearReached;
    public string name;

    public LeaderboardEntry(int yearReached, string name)
    {
        this.yearReached = yearReached;
        this.name = name;
    }
}
