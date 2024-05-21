using System.Collections;
using UnityEngine;
using TMPro;

public class NoColliderPower : MonoBehaviour
{
    public GameObject noColliderTimerPanel;
    public TextMeshProUGUI noColliderTimerText;
    private float powerDuration = 7f;
    private bool powerActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !powerActive)
        {
            powerActive = true;
            StartCoroutine(ActivateNoColliderPower());
        }
    }

    private IEnumerator ActivateNoColliderPower()
    {
        // Tüm "Obs" tag'ine sahip objelerin collider'larını devre dışı bırak
        DisableAllObstacleColliders();

        // Paneli aktif hale getir
        noColliderTimerPanel.SetActive(true);

        // Geri sayımı başlat
        for (int i = (int)powerDuration; i > 0; i--)
        {
            noColliderTimerText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        // Güç süresi bittiğinde collider'ları yeniden etkinleştir
        EnableAllObstacleColliders();

        // Paneli devre dışı bırak
        noColliderTimerPanel.SetActive(false);
        powerActive = false;
    }

    private void DisableAllObstacleColliders()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obs");
        foreach (GameObject obstacle in obstacles)
        {
            Collider collider = obstacle.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }

    private void EnableAllObstacleColliders()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obs");
        foreach (GameObject obstacle in obstacles)
        {
            Collider collider = obstacle.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
            }
        }
    }
}