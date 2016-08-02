using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour {

    public GameObject panel;

	void Start () {
	
	}
	
	void Update () {
	
	}

    void OnTriggerEnter2D()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            panel.SetActive(true);
        }
    }
}
