using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToPlayScene : MonoBehaviour
{
    // Loads the game scene and closes the current scene
    public void ChangeScene()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}