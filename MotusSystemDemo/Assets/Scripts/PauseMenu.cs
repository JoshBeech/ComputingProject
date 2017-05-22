using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public PlayerController Player;
    public Button RestartButton;
    public Button ResumeButton;
    public Button ExitButton;

    private CanvasGroup MenuItems;

    // Use this for initialization
    void Start()
    {
        MenuItems = GetComponentInChildren<CanvasGroup>();
        RestartButton.onClick.AddListener(delegate { Restart(); });
        ResumeButton.onClick.AddListener(delegate { Resume(); });
        ExitButton.onClick.AddListener(delegate { Exit(); });
        MenuItems.interactable = false;
        MenuItems.alpha = 0;
    }

    public void Reveal()
    {
        MenuItems.interactable = true;
        MenuItems.alpha = 1;
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Resume()
    {
        MenuItems.interactable = false;
        MenuItems.alpha = 0;
        Player.enabled = true;
    }

    void Exit()
    {
        Application.Quit();
    }
}
