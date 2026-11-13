using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Current player score
    private static int currentScore = 0;

    // Displays the current score when the scene starts
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = currentScore.ToString(format: "0000");
    }

    // Adds points to the current score
    public void AddScore(int points)
    {
        currentScore += points;
        GetComponent<TextMeshProUGUI>().text = currentScore.ToString(format: "0000");
    }

    // Resets the current score
    public static void ResetScore()
    {
        currentScore = 0;

    }

    // Saves the new best record if the current score is higher
    public static void SaveBestRecord()
    {
        // Loads the saved best record
        int bestRecord = PlayerPrefs.GetInt("BestRecord", 0); 

        if (currentScore > bestRecord)
        {
            PlayerPrefs.SetInt("BestRecord", currentScore);
            PlayerPrefs.Save();
        }
    }
}