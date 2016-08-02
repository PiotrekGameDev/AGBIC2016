using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class StartEvent : MonoBehaviour {

    public UnityEvent onStart;

    void Start () {
        onStart.Invoke();
	}
}
