using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void play()
    {
        SceneManager.LoadScene("Intro");
    }
    public void playWithoutIntro()
    {
        SceneManager.LoadScene("Play");
    }

    public void settings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void exit()
    {
        Application.Quit();
    }
}
