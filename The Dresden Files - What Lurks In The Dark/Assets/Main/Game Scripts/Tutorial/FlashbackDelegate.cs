using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Animator))]
public class FlashbackDelegate : MonoBehaviour
{
    private TutorialFlashbackController flashbackController;
    
    public Animator canvasAnimator; // there should be an animator attached to each canvas group.
    public VoiceLineId voiceLine;
    
    private float waitTime;
    
    private static readonly int fadeParam = Animator.StringToHash("Fade In");
    
    private void Start()
    {
        flashbackController = GetComponentInParent<TutorialFlashbackController>();
    }
    
    public void StartFlashback()
    {
        canvasAnimator.SetBool(fadeParam, true);
    }
    
    public void OnFadeFull()
    {
        StartCoroutine(FlashbackWait());
    }
    
    private IEnumerator FlashbackWait()
    {
        float wait = voiceLine == VoiceLineId.None ? 5 : VoicePlayer.instance.PlayVoiceLine(voiceLine);
        yield return new WaitForSeconds(wait);
        canvasAnimator.SetBool(fadeParam, false);
    }
    
    public void OnFadeEnd() => flashbackController.OnChildFadeEnd();
}
