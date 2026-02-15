using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class SettingsBottons : MonoBehaviour
{
    public GameObject controlPanel;
    public GameObject howToPlayPanel;
    public GameObject musicPanel;

    public GameObject controlsB;
    public GameObject howToPlayB;
    public GameObject musicControlB;
    public GameObject returnB;
    public Slider volumeSlider;
    private MusicController musicController;
    private void Start()
    {
        musicController = FindObjectOfType<MusicController>();

        if (musicController != null && volumeSlider != null)
        {
            volumeSlider.value = musicController.GetComponent<AudioSource>().volume;
        }
    }

    public void controls()
    {
        controlPanel.SetActive(true);
        disableButton();
        activatePanelChildrenRecursively(controlPanel);
    }

    public void howToPlay()
    {
        howToPlayPanel.SetActive(true);
        disableButton();
        activatePanelChildrenRecursively(howToPlayPanel);
    }

    public void musicControl()
    {
        musicPanel.SetActive(true);
        disableButton();
        activatePanelChildrenRecursively(musicPanel);
    }

    private void activatePanelChildrenRecursively(GameObject panel)
    {
        foreach (Transform child in panel.transform)
        {
            child.gameObject.SetActive(true);
            if (child.childCount > 0)
            {
                activatePanelChildrenRecursively(child.gameObject);
            }
        }
    }

    public void updateVolume()
    {
        if (musicController != null && volumeSlider != null)
        {
            musicController.GetComponent<AudioSource>().volume = volumeSlider.value;
        }
    }

    public void returnMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void returnSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void disableButton()
    {
        controlsB.SetActive(false);
        returnB.SetActive(false);
        howToPlayB.SetActive(false);
        musicControlB.SetActive(false);
    }
}
