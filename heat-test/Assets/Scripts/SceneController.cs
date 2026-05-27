using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //0 = test scene 1 = main menu scene


    // Loads a scene by build index
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Loads a scene by name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Reloads the current active scene
    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    // Loads the next scene in the build settings
    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    // Loads the previous scene in the build settings
    public void LoadPreviousScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentIndex > 0)
        {
            SceneManager.LoadScene(currentIndex - 1);
        }
    }

    // Quits the application
    public void QuitGame()
    {
        Debug.Log("Quit Game");

        Application.Quit();
    }
}