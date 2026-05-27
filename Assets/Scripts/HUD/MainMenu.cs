using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit");
    }
}