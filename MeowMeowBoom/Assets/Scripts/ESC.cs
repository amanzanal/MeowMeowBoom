using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ESC : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;

    public void Start()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Asegurar que el juego empieza en velocidad normal
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void pauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
            foreach (Transform child in pausePanel.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public void resumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pausePanel != null)
        {
            foreach (Transform child in pausePanel.transform)
            {
                child.gameObject.SetActive(false);
            }
            pausePanel.SetActive(false);
        }
    }

    public void restartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void backMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
