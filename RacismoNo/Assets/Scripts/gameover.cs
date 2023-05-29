using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
public class gameover : MonoBehaviour
{
    public string sceneName; 
    public void load()
    {
        SceneManager.LoadScene(sceneName);
    }
    
}
