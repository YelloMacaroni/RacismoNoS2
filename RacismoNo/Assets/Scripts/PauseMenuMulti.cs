using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;
using UnityEngine.SceneManagement;
public class PauseMenuMulti : MonoBehaviourPunCallbacks
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (!PhotonNetwork.IsConnected)
            Time.timeScale = 0f;
    }
    
    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        isPaused = false;
        if (!PhotonNetwork.IsConnected)
            Time.timeScale = 1f;
    }
    
    public void RetourAccueil()
    {
        isPaused = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();
        else
            Time.timeScale = 1f;
            SceneManager.LoadScene("Lancement");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("Lancement");
    }
    
    public void Quitter()
    {
        isPaused = false;
        if (PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();
        Application.Quit();
    }
}
