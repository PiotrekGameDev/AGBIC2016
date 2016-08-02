using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PCSwitch : PCSelectable
{

    public UnityEvent switchedOn;
    public UnityEvent switchedOff;
    bool state = false;

    void OnMouseDown()
    {
        if (!state)
        {
            switchedOn.Invoke();
            state = true;
        }
        else {
            switchedOff.Invoke();
            state = false;
        }
    }
}