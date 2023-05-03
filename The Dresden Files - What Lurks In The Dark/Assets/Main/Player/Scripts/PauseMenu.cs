using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    public bool isPaused = false;
    [SerializeField] 
    private GameObject pauseMenu;
    [SerializeField] 
    private PlayerCore player;

    private void Awake()
    {
        instance = this;    
    }

    private void Update()
    {
        // If the player presses the pause button
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Resume the game if paused
            if (PauseMenu.instance.isPaused)
            {
                PauseMenu.instance.Resume();
            }

            // Pause the game if resumed
            else
            {
                PauseMenu.instance.Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        player.InputActions.Pause.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Turn on player mouse
    // Turn off player movement
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        player.InputActions.Pause.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
