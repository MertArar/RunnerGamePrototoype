using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerFalse : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
