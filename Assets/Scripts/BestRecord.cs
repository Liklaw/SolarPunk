using UnityEngine;
using TMPro;

public class BestRecord : MonoBehaviour
{
    // Gets the saved best score and displays it when the scene starts
    private void Start()
    {
        int bestRecord = PlayerPrefs.GetInt("BestRecord", 0);

        GetComponent<TextMeshProUGUI>().text = bestRecord.ToString("0000");
    }
}