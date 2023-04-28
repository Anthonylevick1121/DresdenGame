using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPrompt : MonoBehaviour
{
    private PlayerCore player;
    
    void Start()
    {
        player = FindAnyObjectByType<PlayerCore>();
        StartCoroutine(DisplayTutorial());
    }

    IEnumerator DisplayTutorial()
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
