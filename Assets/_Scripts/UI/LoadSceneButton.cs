using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour {

    public string sceneName;

	void Start ()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene(sceneName);
        });
    }
}
