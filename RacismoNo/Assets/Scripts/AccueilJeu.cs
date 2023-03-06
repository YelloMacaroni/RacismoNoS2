using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccueilJeu : MonoBehaviour
{
    public void LoadLevel(string name)
    {          
        SceneManager.LoadScene(name);
    }

    public void Quitter()
    {
        Application.Quit();
    }
}
