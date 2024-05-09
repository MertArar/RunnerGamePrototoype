using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject planeCoin;
    [SerializeField] private BoxCollider m_collider;
    
    public Transform StartPos;
    public Transform EndPos;

    private float currentCoinPosition;
    
    void Start()
    {
        planeCoin.SetActive(false);
        m_collider.center = new Vector3(m_collider.center.x, m_collider.center.y, m_collider.center.z / transform.localScale.z);
        m_collider.size = new Vector3(m_collider.size.x, m_collider.size.y, m_collider.size.z / transform.localScale.z);
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
