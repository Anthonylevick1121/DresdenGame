using TMPro;
using UnityEngine;

/// <summary>
/// Handles the game UI and any on-screen popups that the player may see.
/// </summary>
[RequireComponent(typeof(PlayerCore))]
public class PlayerUI : MonoBehaviour
{
    // I considered removing this / moving it into another class, however the idea of having all UI-related code here
    // is appealing enough to keep this for now.
    
    [SerializeField] public TextMeshProUGUI promptText;
    [SerializeField] public TextMeshProUGUI tutorialText;
    [SerializeField] public TextMeshProUGUI taskList;
    [SerializeField] public Canvas hudCanvas;
    //[SerializeField] public StatusTextListener status;
    //[SerializeField] public PauseMenuLogic pauseMenu;

    // I have put the execution order of the player UI *after* everything else.
    // If that works how I think it does, I can use this as a hook to start updating the ui only after this is loaded.
    private bool loaded = false;
    public bool IsLoaded() => loaded;
    
    private void Start()
    {
        //hudCanvas.sortingOrder = (int) CanvasLayer.Hud;
        //pauseMenu.GetComponentInParent<Canvas>().sortingOrder = (int) CanvasLayer.PauseScreen;
        loaded = true;
        ObjectiveTracking.instance.OnPlayerUILoad();
    }
}
