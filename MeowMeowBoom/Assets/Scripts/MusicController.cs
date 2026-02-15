using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    private static MusicController instance;
    private AudioSource audioSource;
    public AudioClip playSceneMusic;
    public AudioClip victorySceneMusic;
    public AudioClip menuMusic;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "Menu")
        {
            ChangeMusic(menuMusic);
        }
        else if (scene.name == "Play")
        {
            ChangeMusic(playSceneMusic);
        }
        else if (scene.name == "Victory")
        {
            ChangeMusic(victorySceneMusic);
        }
    }
    public void ChangeMusic(AudioClip newClip)
    {
        if (audioSource.clip != newClip)
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }

    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
