using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrackManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Tiles;
    int randomTile;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerMovement.currentTile == Tiles.Capacity - 1)
        {
            randomTile = Random.Range(0, Tiles.Capacity - 1);
        }
        else
            randomTile = Random.Range(PlayerMovement.currentTile + 1, Tiles.Capacity);
        
        Tiles[randomTile].transform.position = new Vector3(0, 0, Tiles[PlayerMovement.currentTile].transform.position.z + 100);
        PlayerMovement.currentTile = randomTile;
    }
}
