using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenu : MonoBehaviour
{
    // Loads the main menu scene
    public void ChangeScene()
    {
        SceneManager.LoadScene("Menu");

    }
}