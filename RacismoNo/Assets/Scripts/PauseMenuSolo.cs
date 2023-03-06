using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuSolo : MonoBehaviour
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
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }
    
    public void RetourAccueil()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = false;
        SceneManager.LoadScene("Lancement");
    }
    
    public void Quitter()
    {
        isPaused = false;
        Application.Quit();
    }
}