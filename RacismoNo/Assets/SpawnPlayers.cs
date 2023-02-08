using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public void Start()
    {
        Vector3 Position = new Vector3(0, 2, 0);
        PhotonNetwork.Instantiate(playerPrefab.name, Position, Quaternion.identity);
    }
    
}
