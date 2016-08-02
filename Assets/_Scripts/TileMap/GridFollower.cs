using UnityEngine;
using System.Collections;

public class GridFollower : MonoBehaviour {

    public Transform target;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > 1)
        {
            transform.position = new Vector2(Mathf.RoundToInt(target.position.x) + 0.5f, Mathf.RoundToInt(target.position.y));

        }
    }
}
