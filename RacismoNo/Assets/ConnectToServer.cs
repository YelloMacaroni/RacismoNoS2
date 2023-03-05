using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        LoadLobbyScene();
    }
    public void LoadLobbyScene()
    {
        SceneManager.LoadScene("Lobby");
    }
    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    
}
