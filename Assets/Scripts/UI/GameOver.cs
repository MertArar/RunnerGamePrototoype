using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CoinsCollected;
    [SerializeField] private TextMeshProUGUI DistanceRun;

    void OnEnable()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        // Oyun bittiğinde coinleri ve kat edilen mesafeyi güncelle
        CoinsCollected.text = PlayerPrefs.GetInt("CoinsCollected", 0).ToString();
        DistanceRun.text = PlayerPrefs.GetInt("DistanceRun", 0).ToString();
    }

    void Start()
    {
        // Oyun başladığında PlayerPrefs'teki coin değerini sıfırla
        PlayerPrefs.SetInt("CoinsCollected", 0);
        PlayerPrefs.Save();
    }

    public void Retry()
    {
        // Oyunu yeniden başlatırken PlayerPrefs'teki coin verilerini sıfırla
        PlayerPrefs.SetInt("CoinsCollected", 0);
        PlayerPrefs.Save();

        // DistanceRun değerini sıfırlamıyoruz, LevelDistance scripti bunu zaten yapıyor
        SceneManager.LoadScene("Game");
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}