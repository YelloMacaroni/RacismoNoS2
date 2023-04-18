using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Teleportation : MonoBehaviour
{
    public string sceneName; 

    public void Allerauniveau()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void OnTriggerEnter(Collider other)
    {
        Allerauniveau(); 
    }
}