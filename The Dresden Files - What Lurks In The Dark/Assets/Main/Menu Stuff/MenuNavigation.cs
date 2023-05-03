using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}