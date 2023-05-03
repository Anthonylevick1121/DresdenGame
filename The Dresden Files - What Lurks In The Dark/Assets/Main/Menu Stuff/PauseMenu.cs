using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private bool isPaused = false;
    [SerializeField] 
    private Canvas pauseMenu;
    [SerializeField] 
    private PlayerCore player;
    
    private void Start()
    {
        player.InputActions.Pause.performed += ctx => SetPaused(!isPaused);
        pauseMenu.sortingOrder = (int) CanvasLayer.MenuScreen;
    }
    
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void SetPaused(bool pause)
    {
        pauseMenu.gameObject.SetActive(pause);
        isPaused = pause;
        // Time.timeScale = pause ? 0f : 1f;
        player.ToggleGameInput(!pause, true);
        VoicePlayer.instance.SetPaused(pause); // pause/resume audio
    }
    
    /*public void Resume()
    {
        pauseMenu.gameObject.SetActive(false);
        // Time.timeScale = 1f;
        isPaused = false;
        // player.InputActions.Pause.Enable();
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
        
    }
    
    // Turn on player mouse
    // Turn off player movement
    public void Pause()
    {
        pauseMenu.gameObject.SetActive(true);
        // Time.timeScale = 0f;
        isPaused = true;
        player.InputActions.Pause.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }*/
}
