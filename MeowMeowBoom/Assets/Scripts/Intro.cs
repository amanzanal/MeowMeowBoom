using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Intro : MonoBehaviour
{
    public Image blackScreen;
    public TextMeshProUGUI text;

    private float fadeDuration = 1f; // Duración del desvanecimiento del texto.
    private float delayPerCharacter = 0.05f; // Retardo entre cada carácter.
    private float pausePerLine = 1f; // Tiempo que cada línea permanece visible después de mostrarse.

    private string[] fullText =
    {
        "In a dark forest, a white cat swiftly flees from its pursuer.",
        "Behind it, a black cat follows relentlessly,",
        "its eyes glowing like embers.",
        "Every step matters.",
        "Will you manage to escape before being caught?"
    };

    private void Start()
    {
        StartCoroutine(playIntroSequence());
    }

    private IEnumerator playIntroSequence()
    {
        yield return StartCoroutine(showText());
        yield return StartCoroutine(fadeToBlack());
        SceneManager.LoadScene("Play");
    }

    private IEnumerator showText()
    {
        foreach (string line in fullText)
        {
            // Mostrar línea carácter por carácter.
            yield return StartCoroutine(showLine(line));

            // Mantener la línea visible por un tiempo.
            yield return new WaitForSeconds(pausePerLine);

            // Desvanecer la línea antes de pasar a la siguiente.
            yield return StartCoroutine(fadeOutText());
        }
    }

    private IEnumerator showLine(string line)
    {
        text.text = ""; // Asegurarse de que el texto comienza vacío.
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f); // Reiniciar opacidad.

        foreach (char c in line)
        {
            text.text += c; // Agregar un carácter a la vez.
            yield return new WaitForSeconds(delayPerCharacter);
        }
    }

    private IEnumerator fadeOutText()
    {
        float elapsedTime = 0f;
        Color textColor = text.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration)); // Reducir la opacidad.
            text.color = textColor;
            yield return null;
        }

        // Asegurarse de que el texto es completamente transparente.
        textColor.a = 0f;
        text.color = textColor;
    }

    private IEnumerator fadeToBlack()
    {
        blackScreen.gameObject.SetActive(true);

        float elapsedTime = 0f;
        Color screenColor = blackScreen.color;
        screenColor.a = 0f;
        blackScreen.color = screenColor;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            screenColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            blackScreen.color = screenColor;
            yield return null;
        }

        screenColor.a = 1f;
        blackScreen.color = screenColor;
    }

    public void skipIntro()
    {
        SceneManager.LoadScene("Play");
    }
}
