using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public void StartGame()
    {
        ScreenFade.instance.LoadSceneWithFade("Tutorial", true);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}