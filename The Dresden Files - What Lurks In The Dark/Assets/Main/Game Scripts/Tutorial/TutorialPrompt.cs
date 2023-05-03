using System.Collections;
using UnityEngine;

public class TutorialPrompt : MonoBehaviour
{
    [SerializeField]
    private PlayerCore player;
    
    private void Start()
    {
        StartCoroutine(DisplayTutorial());
    }
    
    private IEnumerator DisplayTutorial()
    {
        player.ui.tutorialText.text = "\'W A S D\' to move and \'Shift\' to sprint";
        yield return new WaitForSeconds(5);
        player.ui.tutorialText.text = "\'E\' to interact";
        yield return new WaitForSeconds(5);
        player.ui.tutorialText.text = "\'Tab\' to pause";
        yield return new WaitForSeconds(5);
        player.ui.tutorialText.text = "";
    }
}
