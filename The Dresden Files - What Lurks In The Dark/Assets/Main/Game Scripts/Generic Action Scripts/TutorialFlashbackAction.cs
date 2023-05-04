using UnityEngine;

public class TutorialFlashbackAction : MonoBehaviour
{
    [SerializeField] private TutorialFlashbackController controller;
    [SerializeField] private FlashbackDelegate flashback;
    
    public void RunFlashback()
    {
        controller.RunFlashback(flashback);
    }
}
