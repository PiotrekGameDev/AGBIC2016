using UnityEngine;
using System.Collections;

public class PCMovable : PCSelectable {

    void OnMouseDrag()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
