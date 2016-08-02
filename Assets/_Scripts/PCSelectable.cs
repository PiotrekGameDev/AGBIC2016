using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PCSelectable : MonoBehaviour {

    public bool interactable = true;
    bool mouseOver = false;
    public UnityEvent onMouseOver;
    public UnityEvent onMouseExit;

    public virtual void OnMouseOver()
    {
        if (interactable)
        {
            if (!mouseOver)
            {
                onMouseOver.Invoke();
                mouseOver = true;
            }
        }
        else
        {
            OnMouseExit();
        }
    }
    public virtual void OnMouseExit()
    {
        if (mouseOver)
        {
            onMouseExit.Invoke();
            mouseOver = false;
        }
    }

}
