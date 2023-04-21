using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    public bool isFlickering = false;
    public float timeDelay;
    public float minDelay = 0.1f;
    public float maxDelay = 0.4f;

    // Update is called once per frame
    void Update()
    {
        if (isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    // 
    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
