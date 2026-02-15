using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Points : MonoBehaviour
{
    [SerializeField] private TMP_Text points;
    [SerializeField] private TMP_Text goals;
    [SerializeField] private Image fadeImage;

    private int currentPoints;
    private float fadeDuration = 1f;

    void Start()
    {
        currentPoints = 0;
        updatePoints();
        StartCoroutine(hideTextAfterDelay());
    }

    private void updatePoints()
    {
        if (points != null)
        {
            points.text = "Puntos: " + currentPoints.ToString() + "/100";
        }
    }

    public void updatePointsText(int points)
    {
        currentPoints += points;
        updatePoints();
        victory();
    }

    public void victory()
    {
        if (currentPoints == 100)
        {
            StartCoroutine(playVictoryTransition());
        }
    }

    private IEnumerator hideTextAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        goals.gameObject.SetActive(false);
    }

    private IEnumerator playVictoryTransition()
    {

        fadeImage.gameObject.SetActive(true);
        float elapsedTime = 0f;

        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration); //elapsedTime / fadeDuration
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene("Victory");
    }
}