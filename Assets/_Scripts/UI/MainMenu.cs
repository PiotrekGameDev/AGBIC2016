using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Button playButton;
    public Button exitButton;

    void Start()
    {
        if (playButton != null)
        {
            playButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Game");
            });
        }
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }
    }

}
