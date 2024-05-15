using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDistance : MonoBehaviour
{
    public GameObject disDisplay;
    public GameObject gameOverMenu;
    public static int disRun = 0;
    public bool addingDis = false;
    public float disDelay = 0.35f;
    bool countingStarted = false;

    void Start()
    {
        disDisplay.SetActive(true);
        // Oyun başladığında mesafe değerini sıfırla
        disRun = 0;
        UpdateDistanceDisplay();
    }

    void Update()
    {
        if (!gameOverMenu.activeSelf && Input.GetKeyUp(KeyCode.Space) && !countingStarted)
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

            if (gameOverMenu.activeSelf)
            {
                // Karakter öldüğünde mesafe değerini PlayerPrefs'e kaydet
                PlayerPrefs.SetInt("DistanceRun", disRun);
                PlayerPrefs.Save();
                yield break;
            }

            yield return new WaitForSeconds(disDelay);
        }
    }

    void UpdateDistanceDisplay()
    {
        disDisplay.GetComponent<Text>().text = "" + disRun;
    }
}