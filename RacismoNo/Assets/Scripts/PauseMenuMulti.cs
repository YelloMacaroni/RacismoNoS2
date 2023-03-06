using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;
using UnityEngine.SceneManagement;
public class PauseMenuMulti : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;

    
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
    }
    
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }
    
    public void RetourAccueil()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Lancement");
    }
    
    public void Quitter()
    {
        PhotonNetwork.Disconnect();
        Application.Quit();
    }
}
