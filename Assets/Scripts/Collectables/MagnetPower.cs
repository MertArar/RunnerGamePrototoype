using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MagnetPower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MagnetTimeText;
    [SerializeField] private GameObject MagnetUI;
    [SerializeField] private GameObject Player;
    [SerializeField] private float maxPowerTime;
    private float RemaningPowerTime = -1;
    public static int MagnetEnable = -1;

    void Update()
    {
        if (MagnetEnable == 1)
        {
            if (!MagnetUI.activeInHierarchy)
            {
                MagnetUI.SetActive(true);
                RemaningPowerTime = maxPowerTime;
            }

            if (RemaningPowerTime > 0)
            {
                transform.position = Player.transform.position;
                RemaningPowerTime = RemaningPowerTime - Time.deltaTime;
                MagnetTimeText.text = ((int)RemaningPowerTime).ToString();
            }
            else
            {
                MagnetEnable = -1;
                MagnetUI.SetActive(false);
            }
        }
    }
}
