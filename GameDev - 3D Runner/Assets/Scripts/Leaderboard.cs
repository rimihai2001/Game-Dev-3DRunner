using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard
{

    /*  
     *  LEADERBOARD CLASS
     */

    // Variable used to go through the list of players that have successfully finished the game and reached one of the 
    // top *maxEntries* positions
    public List<LeaderboardEntry> leaderboardEntryList;

    // Variable used to set the maximum of shown shown and saved player entries
    private static int maxEntries = 10;

    // Variable used to access the file where all the leaderboard informations are stored
    private static string filename = "Leaderboard.json";


    public Leaderboard()
    {
        leaderboardEntryList = new List<LeaderboardEntry>();
    }

    /* 
     * LOAD FUNCTION
     */
    public static Leaderboard Load()
    {
        // initializing a new leaderboard
        Leaderboard leaderboard = new Leaderboard();

        // loading the list of entries from the JSON file
        leaderboard.leaderboardEntryList = FileHandler.ReadListFromJSON<LeaderboardEntry>(filename);

        // checking the number of entries so it's not higher than the maximum allowed
        while (leaderboard.leaderboardEntryList.Count > maxEntries)
        {
            // remove the extra entries
            leaderboard.leaderboardEntryList.RemoveAt(maxEntries);
        }
        
        // return the leaderboard
        return leaderboard;
    }

    /*
     * SAVE FUNCTION
     */
    public static void Save(Leaderboard leaderboard)
    {
        // saving the list of entries from the leaderboard used as a parameter in the JSON file
        FileHandler.SaveToJSON<LeaderboardEntry>(leaderboard.leaderboardEntryList, filename);
    }

    /*
     * ADDIFPOSSIBLE FUNCTION
     * this function is used to check all the entries to see if there is one entry that has a lower score than the new entry
     * once an entry with a lower score is found, the new entry is inserted before the old one
     * before saving the changes, the function also removes the extra entries if the number of total entries is higher
     * than the maximum allowed
     * IN ADDITION: the function will return true or false if the new score could enter the leaderboard
     */
    public static bool AddIfPossible(Leaderboard leaderboard, LeaderboardEntry entry)
    {
        // going through all entries
        for (int i = 0; i < maxEntries; i++)
        {
            // checking to see if there is an empty spot for a new entry at the location in the list
            // or if the current score is lower than the new one
            if (i >= leaderboard.leaderboardEntryList.Count || entry.yearReached > leaderboard.leaderboardEntryList[i].yearReached)
            {
                // insert the new entry at the current position
                leaderboard.leaderboardEntryList.Insert(i, entry);

                // remove extra entries 
                while (leaderboard.leaderboardEntryList.Count > maxEntries)
                {
                    leaderboard.leaderboardEntryList.RemoveAt(maxEntries);
                }

                // save the current leaderboard
                Save(leaderboard);

                return true;
            }
        }
        return false;
    }

}