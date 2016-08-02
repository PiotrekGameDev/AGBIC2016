using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndOfLevel : MonoBehaviour {

    public GameObject endLevelMenu;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (SaveSystem.data.unlockedLevels >= sceneIndex)
            {
                SaveSystem.data.unlockedLevels = sceneIndex + 1;
                SaveSystem.Save();
            }
            endLevelMenu.SetActive(true);
        }
    }
}
