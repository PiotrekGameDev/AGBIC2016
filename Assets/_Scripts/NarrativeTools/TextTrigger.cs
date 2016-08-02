using UnityEngine;
using System.Collections;

public class TextTrigger : MonoBehaviour {

    public TextDrawer textDrawer;
    public string message = "...";
    public bool oneUse = true;
    bool used = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!used||!oneUse)
        {
            textDrawer.Say(message);
            used = true;
        }
    }
}
