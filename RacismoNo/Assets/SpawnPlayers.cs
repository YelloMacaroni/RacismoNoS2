using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;


public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public GameObject coordinates;
  
    //public GameObject cat;
    

    public void Start()
    {
        
         Vector3 position = coordinates.transform.position;
        Vector3 Position = new Vector3(position.x, position.y, position.z);  
       //Vector3 Position2 = new Vector3(-23f, 0, 0); 
        if (PhotonNetwork.IsConnected)      
        {
            Debug.Log("connecté");
            GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, Position, Quaternion.identity);
            if ((SceneManager.GetActiveScene()).name == "Floor -1")
            {   
                //var a = GameObject.FindGameObjectsWithTag("cat");
                //if(PhotonNetwork.IsMasterClient)
                //    PhotonNetwork.Instantiate(cat.name, Position2, Quaternion.identity);
                player.transform.localScale = new Vector3(1.2f,1.2f,1.2f);}
            else
            {
                player.transform.localScale = new Vector3(0.7f,0.7f,0.7f);
            }
        }
        else
        {
            GameObject player = Instantiate(playerPrefab, Position, Quaternion.identity); 
            if ((SceneManager.GetActiveScene()).name == "Floor -1")
                {player.transform.localScale = new Vector3(1.2f,1.2f,1.2f);}
            else
            {
                player.transform.localScale = new Vector3(0.7f,0.7f,0.7f);
            }
        }
    }
    
}
