using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
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
    }


    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Turn on player mouse
    // Turn off player movement
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
