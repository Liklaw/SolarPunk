using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    // Total game duration in seconds
    public float gameTime = 120f; 

    // Starts the game countdown
    private void Start()
    {
        StartCoroutine(GameCountdown());
    }

    // Counts down the remaining game time
    private IEnumerator GameCountdown()
    {
        // Current remaining time
        float timer = gameTime; 

        while (timer > 0)
        {
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);

            // Updates the timer display
            GetComponent<TextMeshProUGUI>().text = minutes.ToString("00") + ":" + seconds.ToString("00");

            yield return new WaitForSeconds(1f);

            timer -= 1f;
        }

        // Displays zero when the countdown ends
        GetComponent<TextMeshProUGUI>().text = "00:00";

        // Saves the best score
        ScoreManager.SaveBestRecord();

        // Loads the end screen
        SceneManager.LoadScene("End screen"); 
    }
}