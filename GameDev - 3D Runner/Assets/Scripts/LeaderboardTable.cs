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
    /*
     * 
     * 
     * 
     * 
     */
    void Update()
    {
        if (needRefresh)
        {
            needRefresh = false;
            RefreshUI();
        }
    }
    /*
     * 
     * 
     * 
     * 
     */
    private void RefreshUI()
    {
        if (leaderboardEntryTransformList != null)
        {
            foreach (Transform transform in leaderboardEntryTransformList)
            {
                Destroy(transform.gameObject);
            }
        }
        Leaderboard leaderboard = Leaderboard.Load();
        // Initializing the list
        leaderboardEntryTransformList = new List<Transform>();

        // Going through the entire leaderboard list, all the entries are formatted according to the template
        foreach (LeaderboardEntry leaderboardEntry in leaderboard.leaderboardEntryList)
        {
            CreateHighscoreEntryTransform(leaderboardEntry, entryContainer, leaderboardEntryTransformList);
        }
    }
    /*
     * 
     * 
     * 
     * 
     */
    public static void AddLeaderboardEntry(int yearReached, string name)
    {
        // Create Entry
        LeaderboardEntry leaderboardEntry = new LeaderboardEntry(yearReached, name);

        // Load saved leaderboard from a Json file
        Leaderboard leaderboard = Leaderboard.Load();

        // Add new entry to the leaderboard
        Leaderboard.AddIfPossible(leaderboard, leaderboardEntry);

        needRefresh = true;
    }
    /*
     * 
     * 
     * 
     * 
     */

    private void CreateHighscoreEntryTransform(LeaderboardEntry leaderboardEntry, Transform container, List<Transform> transformList)
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

        int yearReached = leaderboardEntry.yearReached;
        entryTransform.Find("YearReachedEntryText").GetComponent<TextMeshProUGUI>().text = yearReached.ToString();

        string playerName = leaderboardEntry.name;
        entryTransform.Find("NameEntryText").GetComponent<TextMeshProUGUI>().text = playerName;

        entryTransform.Find("Background").gameObject.SetActive(posRank % 2 == 1);

        transformList.Add(entryTransform);
    }

    /*
     * 
     * 
     * 
     * 
     */

}
