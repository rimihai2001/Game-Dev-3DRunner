using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/*
 * LEADERBOARD ENTRY CLASS
 */
public class LeaderboardEntry
{
    // variable used for the score reached
    public int yearReached;
    // variable used for the name selected
    public string name;

    // Constructor used for the entry
    public LeaderboardEntry(int yearReached, string name)
    {
        this.yearReached = yearReached;
        this.name = name;
    }
}
