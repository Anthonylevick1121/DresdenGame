using System;
using UnityEngine;

public class TutorialFlashbackController : MonoBehaviour
{
    //[SerializeField] private Color fadeColor = Color.white;
    [SerializeField] private Canvas flashbackCanvas;
    // we want to fade in a background before the main thing
    [SerializeField] private Animator backgroundAnimator;
    // the flashbacks, which correspond to tutorial tasks.
    // [SerializeField] private FlashbackDelegate[] flashbacks;
    
    private FlashbackDelegate currentFlashback;
    
    private PlayerCore player;
    // private int tasksCompleted;
    
    private static readonly int fadeParam = Animator.StringToHash("Fade In");
    
    private void Start()
    {
        // tasksCompleted = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCore>();
        flashbackCanvas.sortingOrder = (int) CanvasLayer.Flashback;
    }
    
    private void OnTaskComplete(bool required)
    {
        // if (!required || tasksCompleted >= flashbacks.Length) return;
        // run flashback
        player.ToggleGameInput(false, false);
        backgroundAnimator.SetBool(fadeParam, true);
    }
    
    public void RunFlashback(FlashbackDelegate flashback)
    {
        currentFlashback = flashback;
        player.ToggleGameInput(false, false);
        backgroundAnimator.SetBool(fadeParam, true);
    }

    public void OnFadeFull()
    {
        if(currentFlashback) currentFlashback.StartFlashback();
    }
    
    public void OnChildFadeEnd()
    {
        backgroundAnimator.SetBool(fadeParam, false);
    }
    public void OnFadeEnd()
    {
        // tasksCompleted++;
        player.ToggleGameInput(true, false);
        currentFlashback = null;
    }
    
    // private void OnEnable() => ObjectiveTracking.instance.OnTaskComplete += OnTaskComplete;
    // private void OnDisable() => ObjectiveTracking.instance.OnTaskComplete -= OnTaskComplete;
}
