﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool gamePaused = false;

    public GameObject pauseMenuUI;

    public GameObject settingsUI;

    public SetCursor curs;

    void Start()
    {
        pauseMenuUI.SetActive(false); // To not start paused

        settingsUI.SetActive(false); // To not start in settings

        curs = GetComponent<SetCursor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
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
        curs.ChangeCursor(SetCursor.CursorType.shoot);
        pauseMenuUI.SetActive(false);

        settingsUI.SetActive(false);

        Time.timeScale = 1f;

        gamePaused = false; // Set game to resume

    }

    public void SettingsMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
        }

        settingsUI.SetActive(true);

        pauseMenuUI.SetActive(false);

    }

    public void Back()
    {
        settingsUI.SetActive(false);

        pauseMenuUI.SetActive(true);
    }

    void Pause()
    {
        curs.ChangeCursor(SetCursor.CursorType.normal);
        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;

        gamePaused = true; // Set game to be paused

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
