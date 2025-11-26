using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Hospital_Level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
