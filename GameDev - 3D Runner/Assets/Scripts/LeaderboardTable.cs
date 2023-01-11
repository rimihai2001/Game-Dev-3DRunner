using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardTable : MonoBehaviour
{
    // Variable used for LeaderboardEntryContainer
    private Transform entryContainer;
    // Variable used for LeaderboardEntryTemplate
    private Transform entryTemplate;
    // Variable used for the list of entries
    private List<Transform> leaderboardEntryTransformList;

    private static bool needRefresh = false;

    void Awake() {

        // Get container object reference to access their components
        entryContainer = transform.Find("LeaderboardEntryContainer");
        // Get template object reference to access their components
        entryTemplate = entryContainer.Find("LeaderboardEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        needRefresh = true;

    }

    private void CreateHighscoreEntryTransform(LeaderboardEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        // Gets the number of
        int leaderboardListCount = transformList.Count;
        float templateHeight = 25.5f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * leaderboardListCount);
        entryTransform.gameObject.SetActive(true);

        int posRank = leaderboardListCount + 1;
        entryTransform.Find("PositionEntryText").GetComponent<TextMeshProUGUI>().text = posRank.ToString();

        int yearReached = highscoreEntry.yearReached;
        entryTransform.Find("YearReachedEntryText").GetComponent<TextMeshProUGUI>().text = yearReached.ToString();

        string playerName = highscoreEntry.name;
        entryTransform.Find("NameEntryText").GetComponent<TextMeshProUGUI>().text = playerName;

        entryTransform.Find("Background").gameObject.SetActive(posRank % 2 == 1);

        transformList.Add(entryTransform);
    }

    void Update()
    {
        if (needRefresh)
        {
            needRefresh = false;
            RefreshUI();
        }
    }

    private void RefreshUI()
    {
        if (leaderboardEntryTransformList != null)
        {
            foreach (Transform transform in leaderboardEntryTransformList)
            {
                Destroy(transform.gameObject);
            }
        }
        Leaderboard leaderboard = Load();
        // Initializing the list
        leaderboardEntryTransformList = new List<Transform>();

        // Going through the entire leaderboard list, all the entries are formatted according to the template
        foreach (LeaderboardEntry leaderboardEntry in leaderboard.leaderboardEntryList)
        {
            CreateHighscoreEntryTransform(leaderboardEntry, entryContainer, leaderboardEntryTransformList);
        }
    }

    public static void AddLeaderboardEntry(int yearReached, string name)
    {
        // Create Entry
        LeaderboardEntry leaderboardEntry = new LeaderboardEntry(yearReached, name);

        // Load saved leaderboard from a Json file
        Leaderboard leaderboard = Load();

        // Add new entry to the leaderboard
        leaderboard.leaderboardEntryList.Add(leaderboardEntry);

        SortAndSave(leaderboard);

        needRefresh = true;

    }

    private static Leaderboard Load()
    {
        string jsonString = PlayerPrefs.GetString("LeaderboardTable");
        if (JsonUtility.FromJson<Leaderboard>(jsonString) != null)
        {
            return JsonUtility.FromJson<Leaderboard>(jsonString);
        }
        else
            return new Leaderboard();
    }

    private static void SortAndSave(Leaderboard leaderboard)
    {
        // Sorting the leaderboard list, if the list is empty, no need for sorting
        if (leaderboard.leaderboardEntryList.Count != 0)
        {
            SortLeaderboardEntryList(leaderboard.leaderboardEntryList);

            // Save only the first 10 entries
            if (leaderboard.leaderboardEntryList.Count > 10)
            {
                for (int i = leaderboard.leaderboardEntryList.Count-1; i >= 10; i--)
                {
                    leaderboard.leaderboardEntryList.RemoveAt(i);
                }
            }
        }

        // Save new leaderboard into a Json file
        string json = JsonUtility.ToJson(leaderboard);
        PlayerPrefs.SetString("LeaderboardTable", json);
        PlayerPrefs.Save();
    }

    private static void SortLeaderboardEntryList(List<LeaderboardEntry> leaderboardEntryList)
    {
        for (int i = 0; i < leaderboardEntryList.Count; i++)
        {
            for (int j = i + 1; j < leaderboardEntryList.Count; j++)
            {
                if (leaderboardEntryList[j].yearReached > leaderboardEntryList[i].yearReached)
                {
                    LeaderboardEntry temp = leaderboardEntryList[i];
                    leaderboardEntryList[i] = leaderboardEntryList[j];
                    leaderboardEntryList[j] = temp;
                }
            }
        }
    }

    private class Leaderboard
    {
        public List<LeaderboardEntry> leaderboardEntryList;

        public Leaderboard()
        {
            leaderboardEntryList = new List<LeaderboardEntry>();
            leaderboardEntryList.Add(new LeaderboardEntry(0, "BOT"));
        }
    }

    /* 
     *  Leaderboard entry
    */

    [System.Serializable]
    private class LeaderboardEntry
    {
        public int yearReached;
        public string name;

        public LeaderboardEntry(int yearReached, string name)
        {
            this.yearReached = yearReached;
            this.name = name;
        }
    }
}
