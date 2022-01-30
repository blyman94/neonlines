using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

/// <summary>
/// Uses a .php file hosted on BrandonLymanGameDev.com to query and write to
/// a high score database hosted there. Special thanks to Generic Toast at 
/// https://blog.generistgames.com/creating-a-simple-unity-online-leaderboard/
/// who gives an in-depth, step-by-step tutorial on how to set this up.
/// </summary>
public class HighscoreHandler : MonoBehaviour
{
    /// <summary>
    /// URL at which the .php file interfacing with the SQL database is stored.
    /// </summary>
    private const string highscoreURL = 
        "https://brandonlymangamedev.com/neonlineshighscore.php";

    [Tooltip("Signals that the high scores have been loaded from the host " +
        "site.")]
    [SerializeField] private GameEvent highScoresLoaded;

    [Tooltip("String representing the player's name to be posted alongside" +
        "the high score should they achieve leaderboard status.")]
    [SerializeField] private StringVariable currentPlayerName;

    [Tooltip("Current score of the player to be posted alongside their name" +
        "should they achieve leaderboard status.")]
    [SerializeField] private IntVariable currentPlayerScore;

    [Tooltip("Int representing the minimum score necessary to be on the " +
        "leaderboard.")]
    [SerializeField] private IntVariable minHighScore;

    [Tooltip("Array of TextMeshPro objects that will display the leaderboard" +
        "names.")]
    [SerializeField] private TextMeshProUGUI[] namesTextUI;

    [Tooltip("Array of TextMeshPro objects that will display the leaderboard" +
        "scores.")]
    [SerializeField] private TextMeshProUGUI[] scoresTextUI;

    /// <summary>
    /// List of score objects derived from the online leaderboard.
    /// </summary>
    private List<Score> scores;

    private IEnumerator Start()
    {
        yield return RetrieveScoresRoutine();
        int minScoreIndex = Mathf.Min(scores.Count - 1, 4);
        if(minScoreIndex < 0 || scores.Count < 5)
        {
            minHighScore.Value = 0;
            yield break;
        }
        minHighScore.Value = scores[minScoreIndex].score;
    }

    /// <summary>
    /// Starts the LoadHighScores Routine.
    /// </summary>
    public void LoadHighScores()
    {
        StartCoroutine(LoadHighScoresRoutine());
    }

    /// <summary>
    /// Starts the PostScore Routine with the current player's name and score.
    /// </summary>
    public void PostHighScore()
    {
        StartCoroutine(PostScoreRoutine(currentPlayerName.Value, 
            currentPlayerScore.Value));
    }

    /// <summary>
    /// Routine to load the top 5 (or fewer) high scores from the online 
    /// leaderboard.
    /// </summary>
    private IEnumerator LoadHighScoresRoutine()
    {
        yield return RetrieveScoresRoutine();
        int scoresToParse = Mathf.Min(namesTextUI.Length, scores.Count);
        for (int i = 0; i < scoresToParse; i++)
        {
            namesTextUI[i].text = 
                "<color=#FF00F2>" + scores[i].name + "</color>";
            scoresTextUI[i].text = 
                "<color=#00FFFF>" + scores[i].score.ToString() + "</color>";
        }
        highScoresLoaded.Raise();
    }

    /// <summary>
    /// Routine to post a score to the online leaderboard.
    /// </summary>
    /// <param name="name">Name to post to the leaderboard.</param>
    /// <param name="score">Score to post to the leaderboard.</param>
    private IEnumerator PostScoreRoutine(string name, int score)
    {
        // Remove white space from name.
        string processedName = 
            String.Concat(name.Where(c => !Char.IsWhiteSpace(c)));

        // Ensure name is uppercase.
        processedName = processedName.ToUpper();

        // Do not post name if it is empty.
        if (String.IsNullOrEmpty(processedName))
        {
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("post_leaderboard", "true");
        form.AddField("name", processedName);
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post(highscoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.ProtocolError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Successfully posted score!");
            }
        }
    }

    /// <summary>
    /// Routine to gather all scores from the leaderboard.
    /// </summary>
    private IEnumerator RetrieveScoresRoutine()
    {
        scores = new List<Score>();

        WWWForm form = new WWWForm();
        form.AddField("retrieve_leaderboard", "true");

        using (UnityWebRequest www = UnityWebRequest.Post(highscoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.ProtocolError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Successfully retrieved scores!");
                string contents = www.downloadHandler.text;
                using (StringReader reader = new StringReader(contents))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Score entry = new Score();
                        entry.name = line;
                        try
                        {
                            entry.score = Int32.Parse(reader.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Debug.Log("Invalid score: " + e);
                            continue;
                        }

                        scores.Add(entry);
                    }
                }
            }
        }
    }

    
}

public struct Score
{
    public string name;
    public int score;
}