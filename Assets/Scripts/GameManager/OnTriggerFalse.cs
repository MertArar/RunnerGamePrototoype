using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerFalse : MonoBehaviour
{
    public AudioClip coinSound;
    
    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(coinSound, transform.position);
        CollectableControl.coinCount += 1;
        gameObject.SetActive(false);
    }
}
