using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;

public class Teleportation : MonoBehaviourPunCallbacks
{
    public string sceneName; 

    public void Allerauniveau()
    {
        if (!PhotonNetwork.IsConnected)
            SceneManager.LoadScene(sceneName);
        else
            photonView.RPC("RPC_Teleportation", RpcTarget.MasterClient,sceneName);
    }
    public void OnTriggerEnter(Collider other)
    {
        Allerauniveau(); 
    }

    [PunRPC]
    void RPC_Teleportation(string Scene){
            PhotonNetwork.LoadLevel(Scene);    
        }
}