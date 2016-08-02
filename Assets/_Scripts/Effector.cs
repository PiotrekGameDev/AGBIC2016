using UnityEngine;
using System.Collections;

public class Effector : MonoBehaviour {

    public string methodName = "";
    public int value;
    public bool destroyAfterEffect=true;
    public float destroyTime = 0;
    public bool onlyCharacters = false;

	void OnCollisionEnter2D (Collision2D col) {
        if (!onlyCharacters || col.gameObject.layer == 10)
        {
            col.gameObject.SendMessage(methodName, value, SendMessageOptions.DontRequireReceiver);
            if (destroyAfterEffect) Destroy(gameObject, destroyTime);
        }
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!onlyCharacters||col.gameObject.layer == 10)
        {
            col.gameObject.SendMessage(methodName, value, SendMessageOptions.DontRequireReceiver);
            if (destroyAfterEffect) Destroy(gameObject, destroyTime);
        }
    }
}
