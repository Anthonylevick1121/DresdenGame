using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool isPaused = false;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject HUDCanvas;
    [SerializeField]
    private GameObject flashbackCanvas;
    [SerializeField]
    private PlayerCore player;

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToCredits()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Credits");
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        HUDCanvas.SetActive(true);
        flashbackCanvas.SetActive(true);
        isPaused = false;
        player.InputActions.Look.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        HUDCanvas.SetActive(true);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        HUDCanvas.SetActive(false);
        flashbackCanvas.SetActive(false);
        isPaused = true;
        player.InputActions.Look.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "EndScene")
        {
            // If the player presses the pause button
            if (player.InputActions.Pause.IsPressed())
            {
                // Resume the game if paused
                if (isPaused)
                {
                    Resume();
                }

                // Pause the game if resumed
                else
                {
                    Pause();
                }
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}