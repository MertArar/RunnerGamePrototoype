using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject planeCoin;
    public Transform StartPos;
    public Transform EndPos;

    private float currentCoinPosition;
    void Start()
    {
        planeCoin.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentCoinPosition = StartPos.position.z;
        while (currentCoinPosition < EndPos.position.z)
        {
            GameObject Coin = ObjectPool.SharedInstance.GetPooledObject();
            if (Coin != null)
            {
                Coin.transform.position = new Vector3(StartPos.position.x, StartPos.position.y + 1, currentCoinPosition);
                Coin.SetActive(true);
            }

            currentCoinPosition++;
        }
    }

    void Update()
    {
        
    }
}
