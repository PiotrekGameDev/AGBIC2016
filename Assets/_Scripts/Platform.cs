using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col)
    {
        col.transform.parent = transform;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.parent = transform)
        {
            col.transform.parent = null;
        }
    }
}
