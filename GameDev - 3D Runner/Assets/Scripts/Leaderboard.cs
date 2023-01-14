using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard
{
    public List<LeaderboardEntry> leaderboardEntryList;

    private static int maxEntries = 10;
    private static string filename = "Leaderboard.json";

    public Leaderboard()
    {
        leaderboardEntryList = new List<LeaderboardEntry>();
        leaderboardEntryList.Add(new LeaderboardEntry(0, "BOT"));
    }

    public static Leaderboard Load()
    {
        Leaderboard leaderboard = new Leaderboard();
        leaderboard.leaderboardEntryList = FileHandler.ReadListFromJSON<LeaderboardEntry>(filename);

        while (leaderboard.leaderboardEntryList.Count > maxEntries)
        {
            leaderboard.leaderboardEntryList.RemoveAt(maxEntries);
        }

        return leaderboard;
    }

    public static void Save(Leaderboard leaderboard)
    {
        FileHandler.SaveToJSON<LeaderboardEntry>(leaderboard.leaderboardEntryList, filename);
    }


    public static bool AddIfPossible(Leaderboard leaderboard, LeaderboardEntry entry)
    {
        for (int i = 0; i < maxEntries; i++)
        {
            if (i >= leaderboard.leaderboardEntryList.Count || entry.yearReached > leaderboard.leaderboardEntryList[i].yearReached)
            {
                leaderboard.leaderboardEntryList.Insert(i, entry);

                while (leaderboard.leaderboardEntryList.Count > maxEntries)
                {
                    leaderboard.leaderboardEntryList.RemoveAt(maxEntries);
                }

                Save(leaderboard);

                return true;
            }
        }
        return false;
    }

}