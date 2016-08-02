using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextDrawer : MonoBehaviour {

    string toDisplay="";
    Text display;
    int t;
    public float speed=10f;
    float time;
    public float forgetTime = 0.5f;

	// Use this for initialization
	void Start () {
        display = GetComponent<Text>();
	}

    // Update is called once per frame
    void Update()
    {
        if (t < toDisplay.Length)
        {
            time += speed * Time.deltaTime;
            if (time > 1)
            {
                display.text += toDisplay[t];
                t++;
                time = 0;
                if(t==toDisplay.Length)
                {
                    time = 0;
                }
            }
        }
        else {
            time += Time.deltaTime;
            if (time>forgetTime) {
                display.text = "";
                time = 0;
            }
        }
    }

    public void Say(string text)
    {
        toDisplay = text;
        display.text = "";
        t = 0;
    }
}
