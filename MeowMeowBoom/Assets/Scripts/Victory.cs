using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Victory : MonoBehaviour
{
    public Image blackScreen;
    private float fadeDuration = 2f;
    private string victoryText = "Congratulations, you did it!\n\nYou escaped from the black cat. Your quick reflexes and cleverness allowed you to overcome the challenge.\n\nThanks for playing.";

    public TextMeshProUGUI text;
    private float fadeOutDuration = 2f;
    private float delayPerCharacter = 0.05f;
    private float pauseAfterText = 3f;

    private void Start()
    {
        StartCoroutine(PlayVictorySequence());
    }

    private IEnumerator PlayVictorySequence()
    {
        yield return StartCoroutine(ShowText());
        yield return new WaitForSeconds(pauseAfterText);
        yield return StartCoroutine(FadeToBlack());
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator ShowText()
    {
        text.text = "";
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);

        for (int i = 0; i < victoryText.Length; i++)
        {
            text.text += victoryText[i];
            yield return new WaitForSeconds(delayPerCharacter);
        }
    }

    private IEnumerator FadeToBlack()
    {
        blackScreen.gameObject.SetActive(true);

        float elapsedTime = 0f;
        Color color = blackScreen.color;
        color.a = 0f;
        blackScreen.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            blackScreen.color = color;
            yield return null;
        }

        color.a = 1f;
        blackScreen.color = color;
    }

    private IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;
        Color color = text.color;

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1 - (elapsedTime / fadeOutDuration));
            text.color = color;
            yield return null;
        }

        text.text = "";
    }

    public void SkipVictoryScreen()
    {
        SceneManager.LoadScene("Menu");
    }
}
