using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    
    // Start is called before the first frame update
    private void Start()
    {
        bool win = ObjectiveTracking.instance.CheckWin();
        winScreen.SetActive(win);
        loseScreen.SetActive(!win);
    }
}
