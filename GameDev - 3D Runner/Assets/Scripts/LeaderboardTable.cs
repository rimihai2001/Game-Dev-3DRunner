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
    // Variable used to check if there is a need to refresh the UI
    private static bool needRefresh = false;
    /*
     * AWAKE FUNCTION
     */
    void Awake() {

        // Get container object reference to access their components
        entryContainer = transform.Find("LeaderboardEntryContainer");
        // Get template object reference to access their components
        entryTemplate = entryContainer.Find("LeaderboardEntryTemplate");
        // disables the template
        entryTemplate.gameObject.SetActive(false);
        // refresh the UI
        needRefresh = true;

    }
    /*
     * UPDATE FUNCTION
     */
    void Update()
    {
        // if needRefresh = true => Refresh the UI
        if (needRefresh)
        {
            needRefresh = false;
            RefreshUI();
        }
    }
    /*
     * REFRESH UI FUNCTION
     */
    private void RefreshUI()
    {
        // if there the list of the rendered elements is not null, destroy the current elements in the list
        if (leaderboardEntryTransformList != null)
        {
            foreach (Transform transform in leaderboardEntryTransformList)
            {
                Destroy(transform.gameObject);
            }
        }
        // initialize a new leaderboard and load the leaderboard from the JSON file
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
     * ADD LEADERBOARD ENTRY
     */
    public static void AddLeaderboardEntry(int yearReached, string name)
    {
        // Create Entry
        LeaderboardEntry leaderboardEntry = new LeaderboardEntry(yearReached, name);

        // Load saved leaderboard from a Json file
        Leaderboard leaderboard = Leaderboard.Load();

        // Add new entry to the leaderboard if there is space
        Leaderboard.AddIfPossible(leaderboard, leaderboardEntry);

        // Refresh UI
        needRefresh = true;
    }
    /*
     * CREATE HIGHSCORE ENTRY TRANSFORM FUNCTION
     * used to render the leaderboard entries
     */

    private void CreateHighscoreEntryTransform(LeaderboardEntry leaderboardEntry, Transform container, List<Transform> transformList)
    {
        // Gets the number of entries
        int leaderboardListCount = transformList.Count;
        // sets the padding between the entries
        float templateHeight = 25.5f;
        // initialize the template
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        // sets the positions
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * leaderboardListCount);
        // activate the template object
        entryTransform.gameObject.SetActive(true);
        // calculate the position of the entry
        int posRank = leaderboardListCount + 1;
        // displays the position in the template
        entryTransform.Find("PositionEntryText").GetComponent<TextMeshProUGUI>().text = posRank.ToString();
        // retrieves the score
        int yearReached = leaderboardEntry.yearReached;
        // displays the score in the template
        entryTransform.Find("YearReachedEntryText").GetComponent<TextMeshProUGUI>().text = yearReached.ToString();
        // retrieves the name
        string playerName = leaderboardEntry.name;
        // displays the name in the template
        entryTransform.Find("NameEntryText").GetComponent<TextMeshProUGUI>().text = playerName;
        // 2 choies of backgrounds, one light gray and one dark gray
        entryTransform.Find("Background").gameObject.SetActive(posRank % 2 == 1);
        // add the template for the current entry to the list of rendered entries
        transformList.Add(entryTransform);
    }

}
