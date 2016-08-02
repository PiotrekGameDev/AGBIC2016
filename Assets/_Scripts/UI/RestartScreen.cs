using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartScreen : MonoBehaviour
{

    public Button restartButton;
    public Button mainMenuButton;

    void Start()
    {
        mainMenuButton.onClick.AddListener(() => {
            Time.timeScale = 1;
            SceneManager.LoadScene("Menu");
        });
        restartButton.onClick.AddListener(() => {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }
    void OnEnable()
    {
        //Time.timeScale = 0;
    }
    void Update()
    {

    }
}
