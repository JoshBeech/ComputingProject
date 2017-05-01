using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public bool IsFading = false;
    public Image FadeScreen;
    public CanvasRenderer Renderer;

    // Use this for initialization
    void Awake()
    {
        FadeScreen = GetComponentInChildren<Image>();
        FadeScreen.color = Color.black;
        FadeScreen.canvasRenderer.SetAlpha(0.0f);
        Renderer = FadeScreen.canvasRenderer;   
    }

    public void FadeOut()
    {
        IsFading = true;
        FadeScreen.canvasRenderer.SetAlpha(0.0f);
        StartCoroutine(Fade(1.0f));
    }

    private IEnumerator Fade(float p_target)
    {
        FadeScreen.CrossFadeAlpha(p_target, 1.0f, false);
        yield return new WaitForSeconds(1.0f);
        IsFading = false;
    }

    public void FadeIn()
    {
        IsFading = true;
        FadeScreen.canvasRenderer.SetAlpha(1.0f);
        StartCoroutine(Fade(1.0f));
    }
}
