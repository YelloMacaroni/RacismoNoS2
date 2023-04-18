using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject playersolo;

    public void Start()
    {
        Vector3 Position = new Vector3(11.86f, 0, -7);  
        if (PhotonNetwork.IsConnected)      
            {
                Destroy(playersolo);
                PhotonNetwork.Instantiate(playerPrefab.name, Position, Quaternion.identity);
            }
        
    }
    
}
