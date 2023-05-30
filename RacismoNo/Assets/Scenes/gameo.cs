using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;

public class gameo : MonoBehaviour
{
    public string sceneName; 
    public void load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
