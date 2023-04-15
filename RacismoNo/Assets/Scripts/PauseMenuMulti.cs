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
    public GameObject SettingsMenu;
    private bool isPaused;
    private bool isInSettings;

    
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInSettings)
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
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        if (!PhotonNetwork.IsConnected)
            Time.timeScale = 0f;
        if (isInSettings)
            {SettingsMenu.SetActive(false);
            isInSettings = false;}

    }

    public void SettingsOpen()
    {
        isInSettings = true;
        SettingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        isPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        if (!PhotonNetwork.IsConnected)
            Time.timeScale = 0f;
    }
    
    public void ResumeGame()
    {
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        if (!PhotonNetwork.IsConnected)
            Time.timeScale = 1f;
        if (isInSettings)
            {SettingsMenu.SetActive(false);
            isInSettings = false;}
    }
    
    public void RetourAccueil()
    {
        isPaused = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (isInSettings)
            {SettingsMenu.SetActive(false);
            isInSettings = false;}
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
        if (isInSettings)
            {SettingsMenu.SetActive(false);
            isInSettings = false;}
        if (PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();
        Application.Quit();
    }
}
