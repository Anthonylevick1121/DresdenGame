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
        bool win = ObjectiveTracking.instance.CheckWon();
        winScreen.SetActive(win);
        loseScreen.SetActive(!win);
    }
}
