using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public Button resumeButton;
    public Button mainMenuButton;

    void Start()
    {
        mainMenuButton.onClick.AddListener(() => {
            Time.timeScale = 1;
            SceneManager.LoadScene("Menu");
        });
        resumeButton.onClick.AddListener(() => {
            Time.timeScale = 1;
        });
    }
    void OnEnable()
    {
        Time.timeScale = 0;
    }
    void Update()
    {

    }
}
