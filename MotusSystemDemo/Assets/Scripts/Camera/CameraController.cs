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
        FadeScreen.color = Color.black;
        FadeScreen.canvasRenderer.SetAlpha(0.0f);      
    }

    public void FadeOut()
    {        
        FadeScreen.canvasRenderer.SetAlpha(0.0f);
        FadeScreen.CrossFadeAlpha(1.0f, 1.0f, false);
    }

    public void FadeIn()
    {
        FadeScreen.canvasRenderer.SetAlpha(1.0f);
        FadeScreen.CrossFadeAlpha(0.0f, 1.0f, false);

    }
}
