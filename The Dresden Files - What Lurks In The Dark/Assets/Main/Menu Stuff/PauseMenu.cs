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
        ScreenFade.instance.LoadSceneWithFade("MainMenu", false);
        ScreenFade.instance.ManualFadeIn(); // fade fix just in case
    }
    
    public void SetPaused(bool pause)
    {
        pauseMenu.gameObject.SetActive(pause);
        isPaused = pause;
        // Time.timeScale = pause ? 0f : 1f;
        player.ToggleGameInput(!pause, true);
        player.InputActions.Pause.Enable();
        VoicePlayer.instance.SetPaused(pause); // pause/resume audio
    }
}
