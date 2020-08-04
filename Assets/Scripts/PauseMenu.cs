using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    #region variables
    public GameObject pauseMenu;

    public KeyCode pauseKey = KeyCode.Escape;

    bool isPaused;
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Resume();
        FindObjectOfType<LevelLoader>().LoadCurrentLevel();
    }

    public void Menu()
    {
        Resume();
        FindObjectOfType<LevelLoader>().LoadMenu();
    }
}
