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

    private void Start()
    {
        player.InputActions.Pause.performed += ctx => SetPaused(!pauseMenu.activeSelf);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
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

    public void Resume()
    {
        pauseMenu.SetActive(false);
        HUDCanvas.SetActive(true);
        flashbackCanvas.SetActive(true);
        isPaused = false;
        player.InputActions.Enable();
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
        player.InputActions.Disable();
        player.InputActions.Pause.Enable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SetPaused(bool pause)
    {
        if (pause) Pause();
        else Resume();
    }
    
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "EndScene")
        {
            /*
            // If the player presses the pause button
            if (Input.GetKeyDown(KeyCode.Tab))
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
            */
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}