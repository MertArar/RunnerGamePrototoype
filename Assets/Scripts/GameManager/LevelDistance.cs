using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDistance : MonoBehaviour
{
    public GameObject disDisplay;
    public int disRun = 0;
    public bool addingDis = false;
    public float disDelay = 0.35f;
    bool countingStarted = false;

    void Start()
    {
        disDisplay.SetActive(true);
        UpdateDistanceDisplay();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !countingStarted)
        {
            countingStarted = true;
            StartCoroutine(StartCountingDistance());
        }
    }

    IEnumerator StartCountingDistance()
    {
        while (true)
        {
            disRun += 1;
            UpdateDistanceDisplay(); 
            yield return new WaitForSeconds(disDelay);
        }
    }

    void UpdateDistanceDisplay()
    {
        disDisplay.GetComponent<Text>().text = "" + disRun;
    }
}