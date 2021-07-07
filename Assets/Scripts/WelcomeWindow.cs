using UnityEngine;

public class WelcomeWindow : MonoBehaviour
{
    [SerializeField] GameObject gamePlayWindow;
    [SerializeField] GameObject welcomeWindow;

    public void ClickPlayButton()
    {
        welcomeWindow.SetActive(false);
        gamePlayWindow.SetActive(true);
    }
}
