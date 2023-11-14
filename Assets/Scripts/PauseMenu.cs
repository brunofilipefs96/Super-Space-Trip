using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject pauseButton;

    private void Awake()
    {
        pauseButton = GameObject.Find("PauseButton");
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SoundManager.source2.pitch = 1;
        pauseButton.SetActive(true);
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
        SoundManager.source2.pitch *= .5f;
        pauseButton.SetActive(false);
    }

    public void OptionsMenu()
    {
        Debug.Log("Options menu...");
    }

    public void MainMenu()
    {
        Loader.Load(Loader.Scene.MainMenu);
        Time.timeScale = 1;
    }


}
