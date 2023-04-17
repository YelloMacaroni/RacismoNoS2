using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleportation : MonoBehaviour
{
    public string sceneName; 

    public void Allerauniveau()
    {

         SceneManager.LoadScene(sceneName);
    }
    private void OnTriggerEnter(Collider other)
    {
           Allerauniveau(); 
    }
}