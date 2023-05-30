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
            SceneManager.LoadScene(sceneName);
    }
    public void OnTriggerEnter(Collider other)
    {
        Allerauniveau(); 
    }
}