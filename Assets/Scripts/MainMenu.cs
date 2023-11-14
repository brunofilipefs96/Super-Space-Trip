using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        Loader.Load(Loader.Scene.TutorialLevel);
        Time.timeScale = 1;
    }

    public void Options()
    {
        Debug.Log("Options menu...");
    }

}
