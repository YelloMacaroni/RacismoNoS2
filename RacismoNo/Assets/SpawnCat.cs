using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnCat : MonoBehaviourPunCallbacks
{
    public GameObject cat;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 Position2 = new Vector3(-23f, 0, 0); 
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("connecté version pour chat");
            if(PhotonNetwork.IsMasterClient)
                PhotonNetwork.Instantiate(cat.name, Position2, Quaternion.identity);
        }
        else
        {
            Debug.Log("pas connecté version pour chat");
            Instantiate(cat, Position2, Quaternion.identity); 
        }
    }

}
