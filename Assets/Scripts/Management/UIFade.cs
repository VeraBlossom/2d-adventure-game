using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIFade : Singleton<UIFade>
{
    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeSpeed = 1f;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    private IEnumerator fadeRoutine;

    public void FadeToBlack()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }
        fadeRoutine = FadeRoutine(1f);
        StartCoroutine(fadeRoutine);
    }
    public void FadeToClear()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }
        fadeRoutine = FadeRoutine(0f);
        StartCoroutine(fadeRoutine);
    }
    private IEnumerator FadeRoutine(float targetAlpha)
    {
        while(!Mathf.Approximately(fadeScreen.color.a, targetAlpha))
        {
            float Alpha = Mathf.MoveTowards(fadeScreen.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Alpha);
            yield return null;
        }
    }

}
