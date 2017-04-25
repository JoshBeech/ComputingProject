using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private Image FadeScreen;
    private bool Fading = false;
    // Use this for initialization
    void Start()
    {
        FadeScreen = GetComponentInChildren<Image>();
        FadeScreen.canvasRenderer.SetAlpha(0.0f);      
    }

    public void FadeOut()
    {
        FadeScreen.color = Color.black;
        FadeScreen.canvasRenderer.SetAlpha(0.0f);
        StartCoroutine(Fade(1.0f, 1.0f));

    }

    public void FadeIn()
    {
        FadeScreen.color = Color.black;
        FadeScreen.canvasRenderer.SetAlpha(1.0f);
        StartCoroutine(Fade(0.0f, 1.0f));

    }

    IEnumerator Fade(float p_FadeTarget, float p_FadeTime)
    {
        Fading = true;
        FadeScreen.CrossFadeAlpha(p_FadeTarget, p_FadeTime, false);
        yield return new WaitForSeconds(p_FadeTime);
        Fading = false;
    }
}
