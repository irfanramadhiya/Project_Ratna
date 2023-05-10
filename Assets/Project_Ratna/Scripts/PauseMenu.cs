using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;

    public GameObject pauseMenuUI;
    public GameObject introMenuUI;
    public GameObject deathMenuUI;
    public GameObject winMenuUI;

    public RatnaController rct;

    void Start()
    {
        gameIsPaused = false; //game is running, therefore the game is not paused :)
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void Okay() 
    {
        introMenuUI.SetActive(false);
        if (gameIsPaused)
        {
            pauseMenuUI.SetActive(true);
        }
    }

    public void Intro() //for Instructions menu
    {
        introMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    void Pause()
    {  
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        gameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex * 0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}