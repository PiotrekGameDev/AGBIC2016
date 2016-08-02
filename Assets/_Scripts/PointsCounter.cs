using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointsCounter : MonoBehaviour {

    public int points;
    string addon = "X";
    public Text info;

	// Use this for initialization
	void Start () {
	
	}

    public void Add(int amount)
    {
        points += amount;
    }
	
	// Update is called once per frame
	void Update () {
        if (info != null)
        {
            info.text = addon+points.ToString();
        }
	}

    void OnDestroy()
    {
        SaveSystem.data.coins = points;
    }
}
