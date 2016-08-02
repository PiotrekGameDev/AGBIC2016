using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PCButton : PCSelectable
{

    public UnityEvent onClick;
    bool state = false;
    public bool oneUse = false;

    void OnMouseDown()
    {
        if (!state)
        {
            onClick.Invoke();
            if (oneUse)
            {
                state = true;
                interactable = false;
            }
        }
    }
}